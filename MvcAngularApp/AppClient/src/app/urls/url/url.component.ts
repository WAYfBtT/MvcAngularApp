import { Component, Input, OnInit } from '@angular/core';
import { Url } from '../shared/url';
import { ActivatedRoute } from '@angular/router';
import { UrlService } from '../shared/url.service';

@Component({
  selector: 'app-url',
  templateUrl: './url.component.html',
  styleUrls: ['./url.component.css']
})
export class UrlComponent implements OnInit {
  url: Url = {} as Url;

  constructor(private route: ActivatedRoute, private urlService: UrlService) { }

  ngOnInit(): void {
    const idStr = this.route.snapshot.paramMap.get('id');
    const idNum = Number(idStr);
    this.urlService.getUrl(idNum).subscribe(url => {
      this.url = url;
      });
    }
  }