import { TestBed } from '@angular/core/testing';

import { ProfileService } from './Profile.service';

describe('PortfolioService', () => {
  let service: ProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
