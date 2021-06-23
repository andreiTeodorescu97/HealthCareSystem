import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Group } from 'app/_models/group';
import { User } from 'app/_models/user';
import { LastMessagesDto } from 'app/_models/_messages/lastMessagesDto';
import { MessageDto } from 'app/_models/_messages/messageDto';
import { SimpleUserDto } from 'app/_models/_messages/simpleUserDto';
import { environment } from 'environments/environment';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { BusyService } from './busy.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<MessageDto[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient, private busyService: BusyService) { }

  createHubConnection(user: User, otherUsername: string) {
    this.busyService.busy();
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .catch(error => console.log(error))
      .finally(() => this.busyService.idle());

    this.hubConnection.on('ReceiveMessageThread', messages => {
      this.messageThreadSource.next(messages);
    })

    this.hubConnection.on('NewMessage', message => {
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        this.messageThreadSource.next([...messages, message])
      })
    })

    this.hubConnection.on('UpdatedGroup', (group: Group) => {
      if (group.connections.some(x => x.username == otherUsername)) {
        this.messageThread$.pipe(take(1)).subscribe(messages => {
          messages.forEach(message => {
            if (!message.dateRead) {
              message.dateRead = new Date(Date.now());
            }
          })
          this.messageThreadSource.next([...messages]);
        })
      }
    })
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.messageThreadSource.next([]);
      this.hubConnection.stop();
    }
  }

  getLastMessages() {
    return this.http.get<LastMessagesDto[]>(this.baseUrl + 'message/last-messages');
  }

  getUsers() {
    return this.http.get<SimpleUserDto[]>(this.baseUrl + 'message/users');
  }

  getAllThread(userName: string) {
    return this.http.get<MessageDto[]>(this.baseUrl + 'message/thread/' + userName);
  }

  /*   async sendMessage(username: string, content: string) {
      try {
        return this.hubConnection.invoke('SendMessage', { recipientUsername: username, content: content });
      } catch (error) {
        return console.log(error);
      }
    } */

  async sendMessage(username: string, content: string) {
    return this.hubConnection.invoke('SendMessage',
      { recipientUsername: username, content: content })
      .catch(error => console.log(error));
  }
}