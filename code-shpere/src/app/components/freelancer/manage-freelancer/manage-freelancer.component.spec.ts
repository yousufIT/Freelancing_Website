import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageFreelancerComponent } from './manage-freelancer.component';

describe('ManageFreelancerComponent', () => {
  let component: ManageFreelancerComponent;
  let fixture: ComponentFixture<ManageFreelancerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageFreelancerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageFreelancerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
