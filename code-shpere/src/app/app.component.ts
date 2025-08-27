import {
  Component,
  OnInit,
  Renderer2,
  HostListener,
  Inject
} from "@angular/core";
import { CommonModule, Location } from "@angular/common";
import { DOCUMENT } from "@angular/common";
import {  routes } from "./app.routes";
import { Router, RouterModule, RouterOutlet } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { HeaderComponent } from "./components/header/header.component";
import { FooterComponent } from "./components/footer/footer.component";
import { SignalRService } from "./services/signalr.service";

@Component({
  standalone: true,
  selector: "app-root",
  templateUrl: "./app.component.html",
  imports: [
    HeaderComponent,
    FooterComponent,
    RouterOutlet,
    CommonModule,
    FormsModule,
    RouterModule,
],
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  constructor(
    private renderer: Renderer2,
    private signalRService: SignalRService,
    public location: Location
  ) {}
  @HostListener("window:scroll", ["$event"])
  onWindowScroll() {
    if (window.pageYOffset > 100) {
      var element = document.getElementById("navbar-top");
      if (element) {
        element.classList.remove("navbar-transparent");
        element.classList.add("bg-danger");
      }
    } else {
      var element = document.getElementById("navbar-top");
      if (element) {
        element.classList.add("navbar-transparent");
        element.classList.remove("bg-danger");
      }
    }
  }
  ngOnInit() {
    this.onWindowScroll();

    this.signalRService.startConnection();
  }
}

