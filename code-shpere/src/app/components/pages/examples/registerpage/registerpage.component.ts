import { Component, OnInit, OnDestroy, HostListener } from "@angular/core";

@Component({
  selector: "app-registerpage",
  templateUrl: "registerpage.component.html"
})
export class RegisterpageComponent implements OnInit, OnDestroy {
  isCollapsed = true;
  focus: boolean = false;  // Explicitly declare types
  focus1: boolean = false; // Explicitly declare types
  focus2: boolean = false; // Explicitly declare types
  
  constructor() {}

  @HostListener("document:mousemove", ["$event"])
  onMouseMove(e: MouseEvent) {
    var squares1 = document.getElementById("square1");
    var squares2 = document.getElementById("square2");
    var squares3 = document.getElementById("square3");
    var squares4 = document.getElementById("square4");
    var squares5 = document.getElementById("square5");
    var squares6 = document.getElementById("square6");
    var squares7 = document.getElementById("square7");
    var squares8 = document.getElementById("square8");

    var posX = e.clientX - window.innerWidth / 2;
    var posY = e.clientY - window.innerWidth / 6;

    if (squares1) {
      squares1.style.transform = `perspective(500px) rotateY(${posX * 0.05}deg) rotateX(${posY * -0.05}deg)`;
    }
    if (squares2) {
      squares2.style.transform = `perspective(500px) rotateY(${posX * 0.05}deg) rotateX(${posY * -0.05}deg)`;
    }
    // Repeat the same for squares3 to squares8...
  }

  ngOnInit() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("register-page");
  }

  ngOnDestroy() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("register-page");
  }
}
