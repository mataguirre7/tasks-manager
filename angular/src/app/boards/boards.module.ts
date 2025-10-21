import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoardListComponent } from './board-list/board-list.component';
import { BoardDetailsComponent } from './board-details/board-details.component';



@NgModule({
  declarations: [
    BoardListComponent,
    BoardDetailsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BoardsModule { }
