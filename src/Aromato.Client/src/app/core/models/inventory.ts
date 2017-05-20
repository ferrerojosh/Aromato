import { Item } from './item';
export interface Inventory {
  name: string;
  description: string;
  items: Item[];
}
