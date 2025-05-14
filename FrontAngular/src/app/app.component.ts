import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

//@Component({
//  selector: 'app-root',
//  imports: [RouterOutlet],
//  templateUrl: './app.component.html',
//  standalone: false,
//  styleUrl: './app.component.css'
//})
//export class AppComponent {
//  title = 'FrontAngular';
//}


@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './components/auth/login/login.component.html',
  standalone: false,
  /*styleUrl: './app.component.css'*/
})
export class AppComponent {
  title = 'FrontAngular';
}
