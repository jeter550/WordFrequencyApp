import { Component, Output, EventEmitter, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="text-input-container">
      <h2>Word Frequency Analyzer</h2>

      <div class="form-group">
        <label for="textInput">Enter your text (max 2048 characters):</label>
        <textarea
          id="textInput"
          [(ngModel)]="text"
          maxlength="2048"
          placeholder="Paste your text here..."
          [disabled]="isLoading"
        ></textarea>
        <div class="char-count">{{ text.length }} / 2048</div>
      </div>

      <button
        (click)="onAnalyze()"
        [disabled]="isLoading || text.trim().length === 0"
        class="btn-analyze"
      >
        {{ isLoading ? 'Analyzing...' : 'Analyze' }}
      </button>

      <div *ngIf="errorMessage" class="error-message">
        {{ errorMessage }}
      </div>
    </div>
  `,
  styleUrl: './text-input.component.css'
})
export class TextInputComponent {
  @Output() analyze = new EventEmitter<string>();
  @Input() isLoading = false;
  @Input() errorMessage: string | null = null;

  text = '';

  onAnalyze() {
    if (this.text.trim()) {
      this.analyze.emit(this.text);
    }
  }
}
