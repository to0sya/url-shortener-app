import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlViewComponent } from './short-url-view.component';

describe('ShortUrlViewComponent', () => {
  let component: ShortUrlViewComponent;
  let fixture: ComponentFixture<ShortUrlViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShortUrlViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShortUrlViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
