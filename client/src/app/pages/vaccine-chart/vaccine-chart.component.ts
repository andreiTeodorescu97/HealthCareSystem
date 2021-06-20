import { Component, Inject, NgZone, OnInit, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_kelly from "@amcharts/amcharts4/themes/kelly";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { DashboardService } from 'app/_services/dashboard.service';

@Component({
  selector: 'app-vaccine-chart',
  templateUrl: './vaccine-chart.component.html',
  styleUrls: ['./vaccine-chart.component.css']
})
export class VaccineChartComponent implements OnInit {
  private chart: am4charts.XYChart;

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

      let chart = am4core.create("chartdiv2", am4charts.XYChart);
      chart.scrollbarX = new am4core.Scrollbar();

      this.dashboardService.getVaccinesStatistics().subscribe(data => {
        chart.data = data;
      });

      // Create axes
      let categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
      categoryAxis.dataFields.category = "name";
      categoryAxis.renderer.grid.template.location = 0;
      categoryAxis.renderer.minGridDistance = 30;
      categoryAxis.renderer.labels.template.horizontalCenter = "right";
      categoryAxis.renderer.labels.template.verticalCenter = "middle";
      categoryAxis.renderer.labels.template.rotation = 300;
      categoryAxis.tooltip.disabled = true;
      categoryAxis.renderer.minHeight = 150;

      let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
      valueAxis.renderer.minWidth = 50;

      // Create series
      let series = chart.series.push(new am4charts.ColumnSeries());
      series.sequencedInterpolation = true;
      series.dataFields.valueY = "number";
      series.dataFields.categoryX = "name";
      series.tooltipText = "[{categoryX}: bold]{valueY}[/]";
      series.columns.template.strokeWidth = 0;

      series.tooltip.pointerOrientation = "vertical";

      series.columns.template.column.cornerRadiusTopLeft = 10;
      series.columns.template.column.cornerRadiusTopRight = 10;
      series.columns.template.column.fillOpacity = 0.8;

      // on hover, make corner radiuses bigger
      let hoverState = series.columns.template.column.states.create("hover");
      hoverState.properties.cornerRadiusTopLeft = 0;
      hoverState.properties.cornerRadiusTopRight = 0;
      hoverState.properties.fillOpacity = 1;

      series.columns.template.adapter.add("fill", function (fill, target) {
        return chart.colors.getIndex(target.dataItem.index);
      });

      // Cursor
      chart.cursor = new am4charts.XYCursor();
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
