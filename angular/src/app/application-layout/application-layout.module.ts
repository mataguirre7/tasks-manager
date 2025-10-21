import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationLayoutRoutingModule } from './application-layout-routing.module';
import { ApplicationLayoutComponent } from './application-layout.component';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [ApplicationLayoutComponent],
  imports: [
    CommonModule,
    ApplicationLayoutRoutingModule,
    OverlayPanelModule,
    ButtonModule,
  ],
})
export class ApplicationLayoutModule {}
