import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FreelancerReviewComponent } from './freelancer-review.component';

describe('FreelancerReviewComponent', () => {
  let component: FreelancerReviewComponent;
  let fixture: ComponentFixture<FreelancerReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FreelancerReviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FreelancerReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
