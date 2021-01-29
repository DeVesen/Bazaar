import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  private mediaSub: Subscription;

  bodySidebarIsShown = true;
  title = 'DeVes.Bazaar';
  items: MenuItem[];


  constructor(public mediaObserver: MediaObserver) { }


  ngOnInit(): void {
    this.mediaSub = this.mediaObserver.asObservable().subscribe(() => {
      this.bodySidebarIsShown = !this.mediaObserver.isActive('xs')
                             && !this.mediaObserver.isActive('sm');
    });

    this.items = [
      {
        items: [
          {
            label: 'Verkauf',
            icon: 'pi pi-wallet',
            routerLink: '/sale'
          },
          {
            label: 'Händler',
            icon: 'pi pi-users',
            routerLink: '/seller-administration'
          },
          {
            label: 'Statistik',
            icon: 'pi pi-chart-bar',
            routerLink: '/statistics'
          }
        ]
      },
      {
        label: 'Stammdaten',
        items: [
          {
            label: 'Hersteller',
            icon: 'pi pi-tag',
            routerLink: '/manufacturer-administration'
          },
          {
            label: 'Kategorien',
            icon: 'pi pi-tag',
            routerLink: '/category-administration'
          }
        ]
      }
    ];
  }

  ngOnDestroy() {
    this.mediaSub.unsubscribe();
  }

}
