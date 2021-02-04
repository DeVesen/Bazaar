import { Component, OnInit } from '@angular/core';
import { ActivePageInfoService } from 'src/app/services/active-page-info-service/active-page-info.service';

@Component({
  selector: 'app-seller-administration-page',
  templateUrl: './seller-administration-page.component.html',
  styleUrls: ['./seller-administration-page.component.scss']
})
export class SellerAdministrationPageComponent implements OnInit {

  constructor(private activePageInfo: ActivePageInfoService) { }

  ngOnInit(): void {
    this.activePageInfo.setPageTitel('Händler');
  }

}
