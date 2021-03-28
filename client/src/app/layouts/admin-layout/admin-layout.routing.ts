import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserComponent } from '../../pages/user/user.component';
import { TableComponent } from '../../pages/table/table.component';
import { TypographyComponent } from '../../pages/typography/typography.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { NotificationsComponent } from '../../pages/notifications/notifications.component';
import { UpgradeComponent } from '../../pages/upgrade/upgrade.component';
import { RegisterComponent } from '../../pages/register/register.component';
import { AuthGuard } from 'app/guards/auth.guard';
import { NoauthGuard } from 'app/guards/noauth.guard';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user',           component: UserComponent, canActivate:[AuthGuard] },
    { path: 'table',          component: TableComponent, canActivate:[AuthGuard] },
    { path: 'typography',     component: TypographyComponent, canActivate:[AuthGuard] },
    { path: 'icons',          component: IconsComponent, canActivate:[AuthGuard] },
    { path: 'maps',           component: MapsComponent, canActivate:[AuthGuard] },
    { path: 'notifications',  component: NotificationsComponent, canActivate:[AuthGuard] },
    { path: 'upgrade',        component: UpgradeComponent, canActivate:[AuthGuard] },
    { path: 'register',       component: RegisterComponent },
];
