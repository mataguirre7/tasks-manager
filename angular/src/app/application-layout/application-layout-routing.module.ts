import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationLayoutComponent } from './application-layout.component';
import { HomeComponent } from '../home/home.component';
import { ProfileComponent } from '../profile/profile.component';
import { ExampleComponent } from '../example/example.component';
import { SettingsComponent } from '../settings/settings.component';
import { MembersComponent } from '../members/members.component';
import { WorkspaceListComponent } from '../workspaces/workspace-list/workspace-list.component';
import { WorkspaceDetailsComponent } from '../workspaces/workspace-details/workspace-details.component';
import { BoardListComponent } from '../boards/board-list/board-list.component';
import { BoardDetailsComponent } from '../boards/board-details/board-details.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home',
  },
  {
    path: '',
    component: ApplicationLayoutComponent,
    children: [
      {
        path: 'home',
        component: HomeComponent,
      },
      {
        path: 'profile',
        component: ProfileComponent,
      },
      {
        path: 'example',
        component: ExampleComponent,
      },
      {
        path: 'workspaces',
        component: WorkspaceListComponent,
        children: [
          {
            path: ':workspaceId/details',
            component: WorkspaceDetailsComponent,
          },
          {
            path: ':workspaceId/boards',
            component: BoardListComponent,
            children: [
              {
                path: ':boardId/details',
                component: BoardDetailsComponent,
              },
            ],
          },
        ],
      },
      {
        path: 'settings',
        component: SettingsComponent,
      },
      {
        path: 'members',
        component: MembersComponent,
      },
      {
        path: 'boards',
        component: BoardListComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ApplicationLayoutRoutingModule {}
