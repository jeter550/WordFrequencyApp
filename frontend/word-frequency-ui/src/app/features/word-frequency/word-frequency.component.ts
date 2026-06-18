import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisService, AnalyzeResponse } from '../../core/services/analysis.service';
import { InputSelectorComponent } from './components/input-selector/input-selector.component';
import { FrequencyTableComponent } from './components/frequency-table/frequency-table.component';
import { FrequencyChartComponent } from './components/frequency-chart/frequency-chart.component';

@Component({
  selector: 'app-word-frequency',
  standalone: true,
  imports: [CommonModule, InputSelectorComponent, FrequencyTableComponent, FrequencyChartComponent],
  template: `
    <div class="container">
      <app-input-selector
        (analyze)="onAnalyze($event)"
        [isLoading]="isLoading"
        [errorMessage]="errorMessage"
      ></app-input-selector>

      <div *ngIf="result" class="results-container">
        <div class="view-toggle">
          <button
            [class.active]="viewMode === 'table'"
            (click)="viewMode = 'table'"
            class="toggle-btn"
          >
            📊 Table
          </button>
          <button
            [class.active]="viewMode === 'chart'"
            (click)="viewMode = 'chart'"
            class="toggle-btn"
          >
            📈 Chart
          </button>
        </div>

        <app-frequency-table *ngIf="viewMode === 'table'" [data]="result"></app-frequency-table>
        <app-frequency-chart *ngIf="viewMode === 'chart'" [data]="result"></app-frequency-chart>
      </div>
    </div>
  `,
  styleUrl: './word-frequency.component.css'
})
export class WordFrequencyComponent {
  isLoading = false;
  errorMessage: string | null = null;
  result: AnalyzeResponse | null = null;
  viewMode: 'table' | 'chart' = 'table';

  constructor(private analysisService: AnalysisService) {}

  onAnalyze(event: { type: 'text' | 'url'; content: string }) {
    this.isLoading = true;
    this.errorMessage = null;

    const request = event.type === 'text'
      ? this.analysisService.analyzeText(event.content)
      : this.analysisService.analyzeUrl(event.content);

    request.subscribe({
      next: (response) => {
        this.result = response;
        this.isLoading = false;
        this.viewMode = 'table';
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.detail || `An error occurred while analyzing the ${event.type}.`;
        console.error('Analysis error:', error);
      }
    });
  }
}
