import {ComponentFixture, TestBed} from '@angular/core/testing';

import {InputContainerComponent} from './input-container.component';

describe('InputContainerComponent', () => {
  let component: InputContainerComponent;
  let fixture: ComponentFixture<InputContainerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InputContainerComponent]
    });
    fixture = TestBed.createComponent(InputContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
