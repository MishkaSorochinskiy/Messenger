/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FilluserinfoComponent } from './filluserinfo.component';

describe('FilluserinfoComponent', () => {
  let component: FilluserinfoComponent;
  let fixture: ComponentFixture<FilluserinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilluserinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilluserinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
