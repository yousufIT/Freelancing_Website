import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewsForFreelancerComponent } from './reviews-for-freelancer.component';

describe('ReviewsForFreelancerComponent', () => {
  let component: ReviewsForFreelancerComponent;
  let fixture: ComponentFixture<ReviewsForFreelancerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewsForFreelancerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReviewsForFreelancerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
