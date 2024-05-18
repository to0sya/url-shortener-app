import { Routes } from '@angular/router';
import { ShortUrlViewComponent } from './short-url/short-url-view/short-url-view.component';

export const routes: Routes = [
    { path: '', component: ShortUrlViewComponent, pathMatch: 'full' },
];
