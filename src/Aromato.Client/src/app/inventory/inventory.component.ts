import { Component, OnInit } from '@angular/core';
import { Inventory } from '../inventory';
import { InventoryService } from '../inventory.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.scss']
})
export class InventoryComponent implements OnInit {
  inventories: Array<Inventory>;

  constructor(private inventoryService: InventoryService) { }

  ngOnInit() {
    this.inventoryService.findAll().subscribe((inventories) => {
      this.inventories = inventories;
    });
  }

}
