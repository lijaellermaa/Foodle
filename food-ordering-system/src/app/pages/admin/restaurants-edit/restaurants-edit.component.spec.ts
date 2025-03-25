import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantsEditComponent } from './restaurants-edit.component';

describe('RestaurantsDetailsComponent', () => {
  let component: RestaurantsEditComponent;
  let fixture: ComponentFixture<RestaurantsEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RestaurantsEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RestaurantsEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
