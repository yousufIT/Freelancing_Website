import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-toggle-switch',
  templateUrl: './toggle-switch.component.html',
  styleUrls: ['./toggle-switch.component.scss'],
})
export class ToggleSwitchComponent {
  @Input() onColor: string = 'success';
  @Input() offColor: string = 'danger';
  @Input() labelOn: string = 'On';
  @Input() labelOff: string = 'Off';
  isChecked = false;

  toggle() {
    this.isChecked = !this.isChecked;
  }
}
