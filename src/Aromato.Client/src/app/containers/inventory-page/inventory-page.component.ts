import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../../core/services/inventory.service';
import { Observable } from 'rxjs/Observable';
import { Inventory } from '../../core/models/inventory';

@Component({
  selector: 'app-inventory-page',
  templateUrl: './inventory-page.component.html',
  styleUrls: ['./inventory-page.component.scss']
})
export class InventoryPageComponent implements OnInit {
  inventories$: Observable<Inventory[]>;

  constructor(private inventoryService: InventoryService) { }

  ngOnInit() {
    this.inventories$ = this.inventoryService.findAll();
  }

}
