import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillsForProjectComponent } from './skills-for-project.component';

describe('SkillsForProjectComponent', () => {
  let component: SkillsForProjectComponent;
  let fixture: ComponentFixture<SkillsForProjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SkillsForProjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SkillsForProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
