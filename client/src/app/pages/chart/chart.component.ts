import { Component, Inject, NgZone, OnInit, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_kelly from "@amcharts/amcharts4/themes/kelly";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { AppoinmentReasonGraph } from 'app/_models/_dashboard/appoinmentReasonGraph';
import { DashboardService } from 'app/_services/dashboard.service';


@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})

export class ChartComponent implements OnInit {
  private chart: am4charts.PieChart;

  reasonsData: AppoinmentReasonGraph[];

  constructor(@Inject(PLATFORM_ID) private platformId, private zone: NgZone, 
  private dashboardService: DashboardService) { }

  ngOnInit(): void {

  }

  // Run the function only in the browser
  browserOnly(f: () => void) {
    if (isPlatformBrowser(this.platformId)) {
      this.zone.runOutsideAngular(() => {
        f();
      });
    }
  }

  ngAfterViewInit() {
    // Chart code goes in here
    this.browserOnly(() => {
      am4core.useTheme(am4themes_kelly);
      am4core.useTheme(am4themes_animated);

      let chart = am4core.create("chartdiv", am4charts.PieChart);

      this.dashboardService.getReasonsStatistics().subscribe(data => {
        chart.data = data;
      })

      let pieSeries = chart.series.push(new am4charts.PieSeries());
      pieSeries.dataFields.value = "number";
      pieSeries.dataFields.category = "reasonName";

      //chart.innerRadius = am4core.percent(40);

      pieSeries.slices.template.stroke = am4core.color("#0a0a0a");
      pieSeries.slices.template.strokeWidth = 2;
      pieSeries.slices.template.strokeOpacity = 1;  

      chart.legend = new am4charts.Legend();

      this.chart = chart;

    });
  }

  ngOnDestroy() {
    // Clean up chart when the component is removed
    this.browserOnly(() => {
      if (this.chart) {
        this.chart.dispose();
      }
    });
  }
}
