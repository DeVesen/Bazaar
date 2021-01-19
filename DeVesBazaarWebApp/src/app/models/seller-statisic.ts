
export interface ISellerStatistic {
  sellerId: number;

  articles: number;
  onSold: number;
  sold: number;
  returned: number;
  
  sales: number;
  tax: number;
  net: number;
}