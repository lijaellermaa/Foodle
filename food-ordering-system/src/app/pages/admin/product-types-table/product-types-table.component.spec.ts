import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductTypesTableComponent } from './product-types-table.component';

describe('RestaurantsTableComponent', () => {
  let component: ProductTypesTableComponent;
  let fixture: ComponentFixture<ProductTypesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductTypesTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductTypesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
