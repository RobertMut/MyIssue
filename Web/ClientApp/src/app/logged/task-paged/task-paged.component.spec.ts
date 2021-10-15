import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskPagedComponent } from './task-paged.component';

describe('TaskPagedComponent', () => {
  let component: TaskPagedComponent;
  let fixture: ComponentFixture<TaskPagedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskPagedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskPagedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
