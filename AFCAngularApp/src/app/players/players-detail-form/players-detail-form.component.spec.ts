import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayersDetailFormComponent } from './players-detail-form.component';

describe('PlayersDetailFormComponent', () => {
  let component: PlayersDetailFormComponent;
  let fixture: ComponentFixture<PlayersDetailFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlayersDetailFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PlayersDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
