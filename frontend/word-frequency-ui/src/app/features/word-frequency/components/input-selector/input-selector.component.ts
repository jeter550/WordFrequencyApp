import { Component, Output, EventEmitter, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-input-selector',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="input-selector-container">
      <h2>Word Frequency Analyzer</h2>

      <div class="mode-selector">
        <label class="mode-option">
          <input
            type="radio"
            name="inputMode"
            value="text"
            [checked]="inputMode === 'text'"
            (change)="onModeChange('text')"
          >
          <span>Text Input</span>
        </label>
        <label class="mode-option">
          <input
            type="radio"
            name="inputMode"
            value="url"
            [checked]="inputMode === 'url'"
            (change)="onModeChange('url')"
          >
          <span>URL Input</span>
        </label>
      </div>

      <!-- Text Input Mode -->
      <div *ngIf="inputMode === 'text'" class="form-group">
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

      <!-- URL Input Mode -->
      <div *ngIf="inputMode === 'url'" class="form-group">
        <label for="urlInput">Enter a URL:</label>
        <input
          id="urlInput"
          type="url"
          [(ngModel)]="url"
          placeholder="https://example.com"
          [disabled]="isLoading"
        >
      </div>

      <button
        (click)="onAnalyze()"
        [disabled]="isLoading || !isInputValid()"
        class="btn-analyze"
      >
        {{ isLoading ? 'Analyzing...' : 'Analyze' }}
      </button>

      <div *ngIf="errorMessage" class="error-message">
        {{ errorMessage }}
      </div>
    </div>
  `,
  styleUrl: './input-selector.component.css'
})
export class InputSelectorComponent {
  @Output() analyze = new EventEmitter<{ type: 'text' | 'url'; content: string }>();
  @Input() isLoading = false;
  @Input() errorMessage: string | null = null;

  inputMode: 'text' | 'url' = 'text';
  text = '';
  url = '';

  onModeChange(mode: 'text' | 'url') {
    this.inputMode = mode;
    this.errorMessage = null;
  }

  isInputValid(): boolean {
    if (this.inputMode === 'text') {
      return this.text.trim().length > 0;
    } else {
      return this.isValidUrl(this.url.trim());
    }
  }

  onAnalyze() {
    if (this.inputMode === 'text') {
      if (this.text.trim()) {
        this.analyze.emit({ type: 'text', content: this.text });
      }
    } else {
      if (this.isValidUrl(this.url.trim())) {
        this.analyze.emit({ type: 'url', content: this.url });
      }
    }
  }

  private isValidUrl(url: string): boolean {
    try {
      new URL(url);
      return true;
    } catch {
      return false;
    }
  }
}
