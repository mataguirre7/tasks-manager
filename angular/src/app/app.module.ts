import { ApplicationLayoutModule } from './application-layout/application-layout.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeModule } from './home/home.module';
import { AccountModule } from './account/account.module';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from 'primeng/api';
import { OAuthModule } from 'angular-oauth2-oidc';
import { SettingsComponent } from './settings/settings.component';
import { MembersComponent } from './members/members.component';
import { BoardsModule } from './boards/boards.module';
@NgModule({
  declarations: [AppComponent, SettingsComponent, MembersComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    AccountModule,
    HttpClientModule,
    HomeModule,
    BoardsModule,
    ToastrModule.forRoot(),
    OAuthModule.forRoot(),
    BrowserAnimationsModule,
    ApplicationLayoutModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
