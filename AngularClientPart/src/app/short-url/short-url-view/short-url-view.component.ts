import { Component } from '@angular/core';
import { ShortUrlServiceService } from '../short-url-service/short-url-service.service';
import { ShortUrl } from '../short-url';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UrlValidatorDirective } from '../../shared/directives/url-validator.directive';

@Component({
  selector: 'app-short-url-view',
  standalone: true,
  imports: [CommonModule, FormsModule, UrlValidatorDirective],
  templateUrl: './short-url-view.component.html',
  styleUrl: './short-url-view.component.css'
})
export class ShortUrlViewComponent {

  shortUrls: ShortUrl[] = [];

  originalUrl: string = '';

  role: string = '';
  user_id: number = 0;

  constructor(private shortUrlService: ShortUrlServiceService, private httpClient: HttpClient) { }

  ngOnInit() {
    this.loadShortUrls();

    this.httpClient.get('https://localhost:7165/login/info').subscribe((data: any) => {
      console.log(data);
      this.role = data['role'];
      this.user_id = data['id'];
    });
  }

  loadShortUrls() {
    this.shortUrlService.GetShortUrl().subscribe(data => {
      this.shortUrls = data.map((shortUrl: any) => new ShortUrl(shortUrl.id, shortUrl.shortenedUrl, shortUrl.originalUrl, shortUrl.createdBy));
      console.log(this.shortUrls);
    });
  }

  onSubmit(e: Event) {
    e.preventDefault();
    console.log('Form submitted');
    
    this.shortUrlService.AddShortUrl(this.originalUrl, this.user_id).subscribe((data: any) => {
      if (data['success']) {
        this.loadShortUrls();
        
      }
    },
    (e: any) => {
      alert('Error adding short url.' + e.error['message']);
    }
    );
  }

  onDelete(id: number) {
    console.log('Deleting id:', id);
    this.httpClient.delete(`https://localhost:7165/short-url/${id}`).subscribe((data: any) => {
      if (data['success']) {
        alert('Short url deleted successfully.')
        this.loadShortUrls();
      } 
    },
    (e: any) => {
      alert('Error deleting short url.' + e.error['message'])
    }
  );
  }

  onInfo(id: number) {
    console.log('Getting info for id:', id);
    window.location.href = `https://localhost:7165/short-url/${id}` ;
  }
}
