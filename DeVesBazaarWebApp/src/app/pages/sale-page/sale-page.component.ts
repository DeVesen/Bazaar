import { Component, OnInit } from '@angular/core';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';

@Component({
  selector: 'app-sale-page',
  templateUrl: './sale-page.component.html',
  styleUrls: ['./sale-page.component.scss']
})
export class SalePageComponent implements OnInit {

  constructor(private activePageInfo: ActivePageInfoService) { }

  ngOnInit(): void {
    this.activePageInfo.setPageTitel('Verkauf');
  }

}
