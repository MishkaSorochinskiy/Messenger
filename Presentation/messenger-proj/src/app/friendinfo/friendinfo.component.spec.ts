/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FriendinfoComponent } from './friendinfo.component';

describe('FriendinfoComponent', () => {
  let component: FriendinfoComponent;
  let fixture: ComponentFixture<FriendinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FriendinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FriendinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
