import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UrlsComponent } from './urls.component';
import { UrlComponent } from './url/url.component';

const routes: Routes = [
  { path: 'urls', component: UrlsComponent },
  { path: '', redirectTo: 'urls', pathMatch: 'full' },
  { path: 'urls/:id', component: UrlComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UrlsRoutingModule { }
