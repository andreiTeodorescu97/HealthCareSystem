import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from './sidebar.component';
import { HasRoleDirective } from 'app/_directives/has-role.directive';

@NgModule({
    imports: [ RouterModule, CommonModule ],
    declarations: [ SidebarComponent, HasRoleDirective ],
    exports: [ SidebarComponent, HasRoleDirective ]
})

export class SidebarModule {}
