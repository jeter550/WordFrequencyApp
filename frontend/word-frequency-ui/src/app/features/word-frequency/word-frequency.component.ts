import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisService, AnalyzeResponse } from '../../core/services/analysis.service';
import { TextInputComponent } from './components/text-input/text-input.component';
import { FrequencyTableComponent } from './components/frequency-table/frequency-table.component';

@Component({
  selector: 'app-word-frequency',
  standalone: true,
  imports: [CommonModule, TextInputComponent, FrequencyTableComponent],
  template: `
    <div class="container">
      <app-text-input
        (analyze)="onAnalyze($event)"
        [isLoading]="isLoading"
        [errorMessage]="errorMessage"
      ></app-text-input>

      <app-frequency-table [data]="result"></app-frequency-table>
    </div>
  `,
  styleUrl: './word-frequency.component.css'
})
export class WordFrequencyComponent {
  isLoading = false;
  errorMessage: string | null = null;
  result: AnalyzeResponse | null = null;

  constructor(private analysisService: AnalysisService) {}

  onAnalyze(text: string) {
    if (!text.trim()) {
      this.errorMessage = 'Please enter some text to analyze.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;

    this.analysisService.analyzeText(text).subscribe({
      next: (response) => {
        this.result = response;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.detail || 'An error occurred while analyzing the text.';
        console.error('Analysis error:', error);
      }
    });
  }
}
