import { Component, OnDestroy, OnInit, AfterViewInit, AfterContentInit } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { ActivePageInfoService, ITitleActionBtn } from './services/active-page-info-service/active-page-info.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  private mediaSub: Subscription;
  private pageInfoSub: Subscription;

  bodySidebarIsShown = true;
  title = 'DeVes.Bazaar';
  pageTitle = '';
  pageBtns: ITitleActionBtn[];
  items: MenuItem[];


  constructor(public mediaObserver: MediaObserver,
              private activePageInfo: ActivePageInfoService) { }


  ngOnInit(): void {
    this.mediaSub = this.mediaObserver.asObservable().subscribe(x => {
      this.bodySidebarIsShown = !this.mediaObserver.isActive('xs')
                             && !this.mediaObserver.isActive('sm');
      console.log(x[0].mqAlias, x[0].mediaQuery);
    });
    
    this.pageInfoSub = this.activePageInfo.pageTitle$.subscribe(pt => {
      setTimeout(() => {
        this.pageTitle = pt;
      }, 0);
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
    this.pageInfoSub.unsubscribe();
    this.mediaSub.unsubscribe();
  }
}
