import {Component} from '@angular/core';
import {RouterOutlet} from "@angular/router";
import {HeaderComponent} from "./component/header/header.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent
  ],
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'food-ordering-system';
}
