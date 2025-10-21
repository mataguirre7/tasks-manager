import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkspaceListComponent } from './workspace-list/workspace-list.component';
import { WorkspaceDetailsComponent } from './workspace-details/workspace-details.component';



@NgModule({
  declarations: [
    WorkspaceListComponent,
    WorkspaceDetailsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class WorkspacesModule { }
