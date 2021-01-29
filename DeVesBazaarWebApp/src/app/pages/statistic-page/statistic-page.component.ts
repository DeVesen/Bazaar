import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-statistic-page',
  templateUrl: './statistic-page.component.html',
  styleUrls: ['./statistic-page.component.scss']
})
export class StatisticPageComponent implements OnInit {
  articleOptions: any;
  articleData: any;

  sellerOptions: any;
  sellerData: any;
  
  tagsOptions: any;
  tagsData: any;

  constructor() { }

  async ngOnInit(): Promise<void> {
    
    this.articleOptions = this.createChartOptions('Artikel');
    this.articleData = await this.loadArticleData();

    this.sellerOptions = this.createChartOptions('Verkäufer');
    this.sellerData = await this.loadArticleData();

    this.tagsOptions = this.createChartOptions('Artikel / Tags');
    this.tagsData = await this.loadArticleData();
  }

  
  private createChartOptions(title: string): any {
    return {
      title: {
          display: true,
          text: title,
          fontSize: 16,
          fontColor: '#ffffff',
          fontStyle: 'normal'
      },
      legend: {
        display: true,
        position: 'top',
        labels: {
          fontColor: '#ffffff'
        }
      }
    };
  }

  private loadArticleData(): Promise<any> {
    const data = {
      labels: [
        'Nicht im Verkauf',
        'Im Verkauf',
        'Verkauft'
      ],
      datasets: [
          {
              data: [
                300,
                50,
                100
              ],
              backgroundColor: [
                  "#FF6384",
                  "#36A2EB",
                  "#FFCE56"
              ],
              hoverBackgroundColor: [
                  "#b30027",
                  "#1068a2",
                  "#b38000"
              ]
          }]    
      };
    return of(data).toPromise();
  }
}
