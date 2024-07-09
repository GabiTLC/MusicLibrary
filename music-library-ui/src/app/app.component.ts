import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BandListComponent } from './band-list/band-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, BandListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'music-library-ui';
}
