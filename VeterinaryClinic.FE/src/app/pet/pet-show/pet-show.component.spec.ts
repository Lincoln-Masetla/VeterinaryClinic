import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetShowComponent } from './pet-show.component';

describe('PetShowComponent', () => {
  let component: PetShowComponent;
  let fixture: ComponentFixture<PetShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PetShowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PetShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
