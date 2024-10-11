import { Component, OnInit, OnDestroy } from "@angular/core";
import noUiSlider from "nouislider";
import { ToggleSwitchComponent } from "../examples/toggle-switch/toggle-switch.component";

@Component({
  selector: "app-index",
  templateUrl: "index.component.html"
})
export class IndexComponent implements OnInit, OnDestroy {
  isCollapsed = true;
  focus: boolean = false;
  focus1: boolean = false;
  focus2: boolean = false;
  date = new Date();
  pagination = 3;
  pagination1 = 1;

  constructor() {}

  scrollToDownload(element: any) {
    element.scrollIntoView({ behavior: "smooth" });
  }

  ngOnInit() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("index-page");

    // Get the first slider element and check for null
    const slider = document.getElementById("sliderRegular");
    if (slider) {  // Check if slider is not null
      noUiSlider.create(slider, {
        start: 40,
        connect: false,
        range: {
          min: 0,
          max: 100
        }
      });
    } else {
      console.error("Slider element not found.");
    }

    // Get the second slider element and check for null
    const slider2 = document.getElementById("sliderDouble");
    if (slider2) {  // Check if slider2 is not null
      noUiSlider.create(slider2, {
        start: [20, 60],
        connect: true,
        range: {
          min: 0,
          max: 100
        }
      });
    } else {
      console.error("Slider2 element not found.");
    }
  }

  ngOnDestroy() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("index-page");
  }
}
