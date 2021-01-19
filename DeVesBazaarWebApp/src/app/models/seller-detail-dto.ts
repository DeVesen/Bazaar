import { ISeller } from "./seller";
import { ISellerStatistic } from "./seller-statisic";

export interface ISellerDetailDto {
  seller: ISeller;
  statistic: ISellerStatistic;
}