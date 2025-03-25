import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderItemsEditComponent } from './order-items-edit.component';

describe('RestaurantsDetailsComponent', () => {
  let component: OrderItemsEditComponent;
  let fixture: ComponentFixture<OrderItemsEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderItemsEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderItemsEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
