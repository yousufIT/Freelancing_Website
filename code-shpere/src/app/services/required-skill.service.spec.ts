import { TestBed } from '@angular/core/testing';

import { RequiredSkillService } from './required-skill.service';

describe('RequiredSkillService', () => {
  let service: RequiredSkillService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequiredSkillService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
