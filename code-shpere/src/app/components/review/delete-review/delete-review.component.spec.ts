import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteReviewComponent } from './delete-review.component';

describe('DeleteReviewComponent', () => {
  let component: DeleteReviewComponent;
  let fixture: ComponentFixture<DeleteReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteReviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeleteReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
