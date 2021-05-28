import { ChangeDetectionStrategy, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from 'app/_models/user';
import { LastMessagesDto } from 'app/_models/_messages/lastMessagesDto';
import { MessageDto } from 'app/_models/_messages/messageDto';
import { SimpleUserDto } from 'app/_models/_messages/simpleUserDto';
import { AccountService } from 'app/_services/account.service';
import { MessageService } from 'app/_services/message.service';
import { take } from 'rxjs/operators';
import { isTemplateExpression } from 'typescript';
import { ChangeDetectorRef, AfterContentChecked} from '@angular/core';

@Component({
  selector: 'app-messages-page',
  templateUrl: './messages-page.component.html',
  styleUrls: ['./messages-page.component.css']
})
export class MessagesPageComponent implements OnInit {

  user: User;
  allSimpleUsersDto: SimpleUserDto[];
  inboxMessages : LastMessagesDto[];
  thread =  [] as MessageDto[];
  selectedUser = {} as SimpleUserDto;
  @ViewChild('messageForm') messageForm : NgForm;
  messageContent : string;
  loading = false;
  @ViewChild('scrollMe') scrollMe : ElementRef ;  
  scrollTop:number=null;

  constructor(private messageService: MessageService, private accountService: AccountService, private cdref: ChangeDetectorRef) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getUsers();
    this.getLastMessages();
  }

  getLastMessages(){
    this.messageService.getLastMessages().subscribe(data => {
      this.inboxMessages = data;
      this.inboxMessages.forEach(element => {
        element.firstName = this.allSimpleUsersDto.filter(item => item.userName == element.username)[0].firstName;
        element.secondName = this.allSimpleUsersDto.filter(item => item.userName == element.username)[0].secondName;
        element.message.content = element.message.content.substring(0,20) + "...";
      });
    })
  };

  getUsers(){
    this.messageService.getUsers().subscribe(data => {
      this.allSimpleUsersDto = data;
    })
  }

  getThreadForUser(inboxMessage: LastMessagesDto){
    this.selectedUser.firstName = inboxMessage.firstName;
    this.selectedUser.secondName = inboxMessage.secondName;
    this.selectedUser.userName = inboxMessage.username;
    this.selectedUser.mainPhotoUrl = inboxMessage.message.recipientPhotoUrl ? inboxMessage.message.recipientPhotoUrl : inboxMessage.message.senderPhotoUrl;
    this.getAllThread(inboxMessage.username);
  }

  getAllThread(username: string){
    this.messageService.getAllThread(username).subscribe(data => {
        this.thread = data;
    })
  }

  sendMessage(){
    this.loading = true;
    this.messageService.sendMessage(this.selectedUser.userName, this.messageContent)
      .subscribe(message => {
        this.getAllThread(this.selectedUser.userName);
        this.messageForm.reset();
        this.loading = false;
      })
  }


}
