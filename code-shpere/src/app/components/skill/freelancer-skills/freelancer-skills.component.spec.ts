import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FreelancerSkillsComponent } from './freelancer-skills.component';

describe('FreelancerSkillsComponent', () => {
  let component: FreelancerSkillsComponent;
  let fixture: ComponentFixture<FreelancerSkillsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FreelancerSkillsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FreelancerSkillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
