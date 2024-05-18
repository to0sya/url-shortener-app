import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShortUrl } from '../short-url';

@Injectable({
  providedIn: 'root'
})
export class ShortUrlServiceService {
  api_route: string = 'https://localhost:7165/';

  constructor(private httpClient: HttpClient) { }

  GetShortUrl(): Observable<ShortUrl[]> {
    return this.httpClient.get<ShortUrl[]>(`${this.api_route}short-url`);
  }

  AddShortUrl(shortUrl: string, userId: number): any {
    return this.httpClient.post(`${this.api_route}short-url`, { originalUrl: shortUrl, userId: userId });
  }

  DeleteShortUrl(id: number): any {
    return this.httpClient.delete(`${this.api_route}short-url/${id}`);
  }

  GetShortUrlInfo(id: number): void {
    window.location.href = `${this.api_route}short-url/${id}`;
  }
}
