import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductTypesEditComponent } from './product-types-edit.component';

describe('RestaurantsDetailsComponent', () => {
  let component: ProductTypesEditComponent;
  let fixture: ComponentFixture<ProductTypesEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductTypesEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductTypesEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
