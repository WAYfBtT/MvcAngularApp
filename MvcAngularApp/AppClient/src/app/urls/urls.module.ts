import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UrlsRoutingModule } from './urls-routing.module';
import { UrlListComponent } from './url-list/url-list.component';
import { UrlComponent } from './url/url.component';
import { UrlsComponent } from './urls.component';
import { AddUrlComponent } from './add-url/add-url.component';
import { OwnUrlListComponent } from './own-url-list/own-url-list.component';


@NgModule({
  declarations: [
    UrlsComponent,
    UrlListComponent,
    UrlComponent,
    AddUrlComponent,
    OwnUrlListComponent
  ],
  imports: [
    CommonModule,
    UrlsRoutingModule
  ]
})
export class UrlsModule { }
