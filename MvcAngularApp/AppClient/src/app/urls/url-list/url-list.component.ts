import { Component, OnInit } from '@angular/core';
import { Url } from '../shared/url';
import { UrlService } from '../shared/url.service';

@Component({
  selector: 'app-url-list',
  templateUrl: './url-list.component.html',
  styleUrls: ['./url-list.component.css']
})
export class UrlListComponent implements OnInit {

  urls: Url[] = [];
  
  constructor(private urlService: UrlService) { }

  ngOnInit(): void {
    this.GetUrls();
  }

  GetUrls(): void {
    this.urlService.getUrls().subscribe(urls => this.urls = urls);
  }
}
