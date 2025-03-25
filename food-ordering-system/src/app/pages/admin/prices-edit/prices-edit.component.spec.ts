import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PricesEditComponent } from './prices-edit.component';

describe('RestaurantsDetailsComponent', () => {
  let component: PricesEditComponent;
  let fixture: ComponentFixture<PricesEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PricesEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PricesEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
