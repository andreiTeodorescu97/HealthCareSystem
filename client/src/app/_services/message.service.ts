import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LastMessagesDto } from 'app/_models/_messages/lastMessagesDto';
import { MessageDto } from 'app/_models/_messages/messageDto';
import { SimpleUserDto } from 'app/_models/_messages/simpleUserDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getLastMessages() {
    return this.http.get<LastMessagesDto[]>(this.baseUrl + 'message/last-messages');
  }

  getUsers() {
    return this.http.get<SimpleUserDto[]>(this.baseUrl + 'message/users');
  }

  getAllThread(userName : string){
    return this.http.get<MessageDto[]>(this.baseUrl + 'message/thread/' + userName);
  }

  sendMessage(username : string, content : string){
    return this.http.post<MessageDto>(this.baseUrl + 'message', {recipientUsername: username, content: content});
  }
}