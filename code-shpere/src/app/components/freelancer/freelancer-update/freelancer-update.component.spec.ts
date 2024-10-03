import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FreelancerUpdateComponent } from './freelancer-update.component';

describe('FreelancerUpdateComponent', () => {
  let component: FreelancerUpdateComponent;
  let fixture: ComponentFixture<FreelancerUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FreelancerUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FreelancerUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
