import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { ToastrModule } from "ngx-toastr";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SidebarModule } from './sidebar/sidebar.module';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule} from './shared/navbar/navbar.module';
import { FixedPluginModule} from './shared/fixedplugin/fixedplugin.module';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

import { CommonModule } from '@angular/common';

import { DashboardComponent }       from './pages/dashboard/dashboard.component';
import { UserComponent }            from './pages/user/user.component';
import { TableComponent }           from './pages/table/table.component';
import { TypographyComponent }      from './pages/typography/typography.component';
import { IconsComponent }           from './pages/icons/icons.component';
import { MapsComponent }            from './pages/maps/maps.component';
import { NotificationsComponent }   from './pages/notifications/notifications.component';
import { UpgradeComponent }         from './pages/upgrade/upgrade.component';
import { RegisterComponent }        from './pages/register/register.component';
import { HomeComponent }        from './pages/home/home.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from "./app-routing.module";


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    UserComponent,
    TableComponent,
    TypographyComponent,
    IconsComponent,
    MapsComponent,
    NotificationsComponent,
    RegisterComponent,
    UpgradeComponent,
    TextInputComponent,
    HomeComponent
  ],
  imports: [
    BrowserAnimationsModule,
    AppRoutingModule,
    SidebarModule,
    NavbarModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      closeButton: true,
      enableHtml: true,
      positionClass: "toast-bottom-right"
    }),
    FooterModule,
    FixedPluginModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
