import { Component, OnInit, OnDestroy } from "@angular/core";
import { Chart } from "chart.js";

@Component({
  selector: "app-landingpage",
  templateUrl: "landingpage.component.html"
})
export class LandingpageComponent implements OnInit, OnDestroy {
  isCollapsed = true;
  constructor() {}

  ngOnInit() {
    const body = document.getElementsByTagName("body")[0];
    body.classList.add("landing-page");

    const canvas: any = document.getElementById("chartBig");
    const ctx = canvas.getContext("2d");
    const gradientFill = ctx.createLinearGradient(0, 350, 0, 50);
    gradientFill.addColorStop(0, "rgba(228, 76, 196, 0.0)");
    gradientFill.addColorStop(1, "rgba(228, 76, 196, 0.14)");

    const chartBig = new Chart(ctx, {
      type: "line",
      data: {
        labels: [
          "JUN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
        ],
        datasets: [
          {
            label: "Data",
            fill: true,
            backgroundColor: gradientFill,
            borderColor: "#e44cc4",
            borderWidth: 2,
            pointBackgroundColor: "#e44cc4",
            pointBorderColor: "rgba(255,255,255,0)",
            pointHoverBackgroundColor: "#be55ed",
            pointBorderWidth: 20,
            pointHoverRadius: 4,
            pointHoverBorderWidth: 15,
            pointRadius: 4,
            data: [80, 160, 200, 160, 250, 280, 220, 190, 200, 250, 290, 320]
          }
        ]
      },
      options: {
        maintainAspectRatio: false,
        plugins: {
          legend: {
            display: false
          },
          tooltip: {
            backgroundColor: "#fff",
            titleColor: "#ccc",
            bodyColor: "#666",
            bodySpacing: 4,
            padding: 12,
            mode: "nearest",
            intersect: false,
            position: "nearest"
          }
        },
        scales: {
          y: {
            beginAtZero: true,
            min: 0,
            max: 350,
            grid: {
              color: "rgba(0,0,0,0.0)" // Adjust grid line color
            },
            ticks: {
              display: false,
              padding: 20,
              color: "#9a9a9a"
            }
          },
          x: {
            grid: {
              color: "rgba(0,0,0,0.0)" // Adjust grid line color
            },
            ticks: {
              padding: 20,
              color: "#9a9a9a"
            }
          }
        }
      }
    });
  }

  ngOnDestroy() {
    const body = document.getElementsByTagName("body")[0];
    body.classList.remove("landing-page");
  }
}
