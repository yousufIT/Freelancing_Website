import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BidUpdateComponent } from './bid-update.component';

describe('BidUpdateComponent', () => {
  let component: BidUpdateComponent;
  let fixture: ComponentFixture<BidUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BidUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BidUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
