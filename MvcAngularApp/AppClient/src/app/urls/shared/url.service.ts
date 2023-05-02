import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Url } from './url';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  baseUrl = 'https://localhost:44385/api/url';

  constructor(private http : HttpClient) { }

  createUrl(url: Url): Observable<Url> {
    return this.http.post<Url>(`${this.baseUrl}`, url);
  }

  // Get a single Url by id
  getUrl(id: number): Observable<Url> {
    return this.http.get<Url>(`${this.baseUrl}/${id}`);
  }

  // Get all Urls
  getUrls(): Observable<Url[]> {
    return this.http.get<Url[]>(`${this.baseUrl}/all`);
  }

  // Get all own Urls
  getOwnUrls(): Observable<Url[]> {
    return this.http.get<Url[]>(`${this.baseUrl}/own`);
  }

  // Delete a Url by id
  deleteByIdUrl(id: number): Observable<Url> {
    return this.http.delete<Url>(`${this.baseUrl}/${id}`);
  }

  // Delete a Url Url
  deleteUrl(url: Url): Observable<Url> {
    return this.http.delete<Url>(`${this.baseUrl}`, {body: url});
  }
}
