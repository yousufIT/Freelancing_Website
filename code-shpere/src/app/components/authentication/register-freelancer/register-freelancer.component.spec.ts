import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterFreelancerComponent } from './register-freelancer.component';

describe('RegisterFreelancerComponent', () => {
  let component: RegisterFreelancerComponent;
  let fixture: ComponentFixture<RegisterFreelancerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterFreelancerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterFreelancerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
