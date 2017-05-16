import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginRoleComponent } from './login-role.component';

describe('LoginRoleComponent', () => {
  let component: LoginRoleComponent;
  let fixture: ComponentFixture<LoginRoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginRoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
