import { animate, state, style, transition, trigger } from '@angular/animations';
import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ISeller } from 'src/app/models/seller';
import { ISellerStatistic } from 'src/app/models/seller-statisic';
import { StatisticsApi } from 'src/app/services/statistics-api-service/statistics-api.service';
import { SellerApiService } from 'src/app/services/seller-api-service/seller-api.service';


@Component({
  selector: 'app-seller-table',
  templateUrl: './seller-table.component.html',
  styleUrls: ['./seller-table.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class SellerTableComponent implements AfterViewInit {
  displayedColumns: string[] = ['id', 'name'];
  dataSource = new MatTableDataSource([]);
  expandedElement: ISeller | null;

  @ViewChild(MatSort) sort: MatSort;

  @Input() stickyHeader: boolean = true;
  @Input() stickyColumnId: boolean = true;


  constructor(
    private _sellerApi: SellerApiService) {
    this._sellerApi.dataUpdated.subscribe(() => this.reloadRows());
  }

  
  async ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    await this.reloadRows();
  }

  onActionBtnClicked(key: string, number: number): void {
    console.log(key, number);
  }
  

  private async reloadRows(): Promise<void> {
    this.dataSource.data = await this._sellerApi.getAll();
  }

  public getDisplayName(element: ISeller): string {
    return `${element.lastName}, ${element.firstName}`;
  }
}
