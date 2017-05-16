import {Item} from "./item";

export class Inventory {
  id: number;
  name: string;
  description: string;
  items: Array<Item>;
}
