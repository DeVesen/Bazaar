import { Component, Input } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';

@Component({
  selector: 'app-header-menu-btn',
  templateUrl: './header-menu-btn.component.html',
  styleUrls: ['./header-menu-btn.component.scss']
})
export class HeaderMenuBtnComponent {

  @Input() caption: string;

  constructor(public mediaObserver: MediaObserver) { }
}
