import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavMenuLoggedComponent } from './nav-menu-logged.component';

describe('NavMenuLoggedComponent', () => {
  let component: NavMenuLoggedComponent;
  let fixture: ComponentFixture<NavMenuLoggedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavMenuLoggedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuLoggedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
