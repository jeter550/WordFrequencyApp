import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartConfiguration, ChartData } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { AnalyzeResponse, WordCount } from '../../../../core/services/analysis.service';

@Component({
  selector: 'app-frequency-chart',
  standalone: true,
  imports: [CommonModule, BaseChartDirective],
  template: `
    <div class="chart-container" *ngIf="data">
      <div class="chart-controls">
        <label>
          <input type="radio" name="chartType" value="bar" [checked]="chartType === 'bar'" (change)="onChartTypeChange('bar')">
          Bar Chart
        </label>
        <label>
          <input type="radio" name="chartType" value="bubble" [checked]="chartType === 'bubble'" (change)="onChartTypeChange('bubble')">
          Bubble Chart
        </label>
      </div>

      <div *ngIf="chartType === 'bar'" class="bar-chart-wrapper">
        <canvas
          baseChart
          [type]="'barChart'"
          [data]="barChartData"
          [options]="barChartOptions"
          [plugins]="plugins"
          aria-label="Word Frequency Bar Chart"
        ></canvas>
      </div>

      <div *ngIf="chartType === 'bubble'" class="bubble-chart-wrapper">
        <canvas
          baseChart
          [type]="'bubbleChart'"
          [data]="bubbleChartData"
          [options]="bubbleChartOptions"
          [plugins]="plugins"
          aria-label="Word Frequency Bubble Chart"
        ></canvas>
      </div>
    </div>
  `,
  styleUrl: './frequency-chart.component.css'
})
export class FrequencyChartComponent implements OnChanges {
  @Input() data: AnalyzeResponse | null = null;

  chartType: 'bar' | 'bubble' = 'bar';
  plugins = [];

  barChartData: ChartData<'bar'> = { labels: [], datasets: [] };
  barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    maintainAspectRatio: true,
    indexAxis: 'y',
    scales: {
      x: {
        beginAtZero: true,
        ticks: {
          callback: (value) => Math.round(Number(value)).toString()
        }
      }
    }
  };

  bubbleChartData: ChartData<'bubble'> = { datasets: [] };
  bubbleChartOptions: ChartConfiguration<'bubble'>['options'] = {
    responsive: true,
    maintainAspectRatio: true,
    scales: {
      x: {
        beginAtZero: true,
        title: { display: true, text: 'Word Rank' }
      },
      y: {
        beginAtZero: true,
        title: { display: true, text: 'Frequency' }
      }
    }
  };

  ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && this.data) {
      this.updateCharts();
    }
  }

  onChartTypeChange(type: 'bar' | 'bubble') {
    this.chartType = type;
  }

  private updateCharts() {
    if (!this.data) return;

    const topWords = this.data.results.slice(0, 20);

    this.updateBarChart(topWords);
    this.updateBubbleChart(this.data.results);
  }

  private updateBarChart(words: WordCount[]) {
    this.barChartData = {
      labels: words.map(w => w.word),
      datasets: [
        {
          label: 'Frequency',
          data: words.map(w => w.count),
          backgroundColor: 'rgba(76, 175, 80, 0.7)',
          borderColor: 'rgba(76, 175, 80, 1)',
          borderWidth: 1
        }
      ]
    };
  }

  private updateBubbleChart(words: WordCount[]) {
    const bubbleData = words.map(w => ({
      x: w.rank,
      y: w.count,
      r: Math.max(5, Math.min(30, w.count / 2))
    }));

    this.bubbleChartData = {
      datasets: [
        {
          label: 'Word Frequency',
          data: bubbleData,
          backgroundColor: 'rgba(76, 175, 80, 0.6)',
          borderColor: 'rgba(76, 175, 80, 1)',
          borderWidth: 1
        }
      ]
    };
  }
}
