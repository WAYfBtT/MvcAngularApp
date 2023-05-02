import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { UrlsRoutingModule } from './urls/urls-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { UrlsModule } from './urls/urls.module';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    UrlsRoutingModule,
    AppRoutingModule,
    UrlsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
