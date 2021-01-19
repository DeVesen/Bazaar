import { Component, OnInit, ViewChild, AfterViewInit, Input, EventEmitter, Output } from '@angular/core';
import {FormControl} from '@angular/forms';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { ISeller } from 'src/app/models/seller';
import { SellerApiService } from 'src/app/services/seller-api-service/seller-api.service';

@Component({
  selector: 'app-seller-selection-input',
  templateUrl: './seller-selection-input.component.html',
  styleUrls: ['./seller-selection-input.component.scss']
})
export class SellerSelectionInputComponent implements OnInit, AfterViewInit {
  
  filteredSellers: Observable<ISeller[]>;
  sellers: ISeller[] = [];
  inputHint: string;
  
  @Input() inputLabel: string;
  @Input() sellerCtrl: FormControl;
  @Input() showSellerHint: boolean;
  @Output() inputChange = new EventEmitter<ISeller>();
  @ViewChild(MatAutocompleteTrigger, { static: true }) matAutocompleteTrigger: MatAutocompleteTrigger;


  constructor(private _sellerService: SellerApiService) {
    this._sellerService.getAll().then(x => this.sellers = x);
  }

  ngOnInit(): void {
    this.filteredSellers = this.sellerCtrl.valueChanges
      .pipe(
        startWith(''),
        map(sellerItem => sellerItem ? this._filterStates(sellerItem) : this.sellers.slice())
      );
  }
  
  ngAfterViewInit(): void {
  }


  optionSelected(): void {
    this._setInputHint();
  }

  keyUp(event$: any): void {
    if (event$.key === 'Enter') {
      this.matAutocompleteTrigger.closePanel();
      this._setInputHint();
    } else {
      this._sellerService.getAll().then(x => this.sellers = x);
    }
  }
  

  public getErrorMessage(formCtrl: FormControl): string {
    if (formCtrl.hasError('required')) {
      return 'Das Feld darf nicht leer sein!';
    }
    if (formCtrl.hasError('email')) {
      return 'Kein korrektes E-Mail Format!';
    }
    if (formCtrl.hasError('min')) {
      return `Der wert muss größer ${formCtrl.getError('min').min} sein!`;
    }
    
    if (formCtrl.hasError('numberInUse')) {
      return `Nummer ${formCtrl.getError('numberInUse').actual} ist bereits vergeben!`;
    }
    if (formCtrl.hasError('numberNotInUse')) {
      return `Nummer ${formCtrl.getError('numberNotInUse').actual} ist nicht vergeben!`;
    }

    return ''
  }


  private _setInputHint(): void {
    const sellerId = +this.sellerCtrl.value; 
    const selectedSeller = this.sellers.find(s => s.id == sellerId);

    this.inputHint = selectedSeller
        ? `${selectedSeller.lastName}, ${selectedSeller.firstName}`
        : '';
    this.inputChange.emit(selectedSeller);
  }

  private _filterStates(value: string): ISeller[] {
    if (value) {
      const filterValue = value.toString().toLowerCase();
      return this.sellers.filter(sellerItem => {
        return sellerItem.id.toString().indexOf(filterValue) === 0 ||
               sellerItem.lastName.toLowerCase().indexOf(filterValue) === 0 ||
               sellerItem.firstName.toLowerCase().indexOf(filterValue) === 0;
      });
    }
    return this.sellers;
  }
}
