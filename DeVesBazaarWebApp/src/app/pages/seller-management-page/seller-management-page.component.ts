import { Component, OnInit } from "@angular/core";
import { SellerUiActionsService } from "src/app/services/seller-ui-actions/seller-ui-actions.service";



@Component({
  selector: 'app-seller-management-page',
  templateUrl: './seller-management-page.component.html',
  styleUrls: ['./seller-management-page.component.scss']
})
export class SellerManagementPageComponent implements OnInit {
  
  constructor(
    private _sellerUiActions: SellerUiActionsService) { }


  ngOnInit(): void {
  }
  

  public appendNewSeller(): void {
    this._sellerUiActions.newSeller();
  }
}
