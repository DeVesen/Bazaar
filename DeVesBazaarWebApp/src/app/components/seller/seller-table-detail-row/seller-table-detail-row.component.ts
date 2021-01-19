import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { SellerUiActionsService } from 'src/app/services/seller-ui-actions/seller-ui-actions.service';
import { ISeller } from 'src/app/models/seller';
import { ISellerStatistic } from '../../../models/seller-statisic';
import { StatisticsApi } from 'src/app/services/statistics-api-service/statistics-api.service';


@Component({
  selector: 'app-seller-table-detail-row',
  templateUrl: './seller-table-detail-row.component.html',
  styleUrls: ['./seller-table-detail-row.component.scss']
})
export class SellerTableDetailRowComponent implements OnInit, OnChanges {
  
  public statistics: ISellerStatistic;

  @Input() seller: ISeller;


  constructor(
    public statisticsApi: StatisticsApi,
    public sellerUiActions: SellerUiActionsService) { }

  ngOnInit(): void {
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    this.statisticsApi.get(this.seller.id).then(x => {
      this.statistics = x;
      console.log(x);
    });
  }


  public updateSellerUi(sellerId: number): void {
    this.sellerUiActions.updateSellerUi(sellerId);
  }

  public navigateToArticels(sellerId: number): void {
    this.sellerUiActions.navigateToArticels(sellerId);
  }

  public deleteSellerUi(sellerId: number): void {
    this.sellerUiActions.deleteSellerUi(sellerId);
  }
}
