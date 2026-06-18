import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { WordFrequencyComponent } from './features/word-frequency/word-frequency.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, WordFrequencyComponent],
  template: `<app-word-frequency></app-word-frequency>`,
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Word Frequency App';
}
