export interface ProductModel {
  id: number; // product ID
  name: string; // product name
  description?: string; //optional
  price: number;        // product price
  modifiedAt?: string;  // opcional, date of last modification
}
