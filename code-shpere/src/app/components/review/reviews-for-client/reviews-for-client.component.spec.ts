import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewsForClientComponent } from './reviews-for-client.component';

describe('ReviewsForClientComponent', () => {
  let component: ReviewsForClientComponent;
  let fixture: ComponentFixture<ReviewsForClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewsForClientComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReviewsForClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
