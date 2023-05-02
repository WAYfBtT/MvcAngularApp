import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnUrlListComponent } from './own-url-list.component';

describe('OwnUrlListComponent', () => {
  let component: OwnUrlListComponent;
  let fixture: ComponentFixture<OwnUrlListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnUrlListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnUrlListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
