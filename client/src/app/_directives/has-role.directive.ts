import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { take } from 'rxjs/operators';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {

  @Input() appHasRole: string[];
  user: User;

  constructor(private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
    //clear view if no roles
    if (!this.user?.roles || this.user == null) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
      return;
    }
    if (this.appHasRole[0] == undefined) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
      return;
    }
    let roleExists = false;
    this.user?.roles.forEach(element => {
      if (this.appHasRole[0].includes(element)) {
        roleExists = true;
      }
    });
    if (roleExists) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }
}
