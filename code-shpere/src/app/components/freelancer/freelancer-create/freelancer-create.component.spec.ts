import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FreelancerCreateComponent } from './freelancer-create.component';

describe('FreelancerCreateComponent', () => {
  let component: FreelancerCreateComponent;
  let fixture: ComponentFixture<FreelancerCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FreelancerCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FreelancerCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
