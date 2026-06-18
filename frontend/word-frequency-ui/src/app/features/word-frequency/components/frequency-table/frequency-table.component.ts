import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalyzeResponse, WordCount } from '../../../../core/services/analysis.service';

@Component({
  selector: 'app-frequency-table',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div *ngIf="data" class="table-container">
      <div class="summary">
        <div class="summary-item">
          <strong>Total Words:</strong> {{ data.totalWords }}
        </div>
        <div class="summary-item">
          <strong>Unique Words:</strong> {{ data.uniqueWords }}
        </div>
      </div>

      <table>
        <thead>
          <tr>
            <th>Rank</th>
            <th>Word</th>
            <th>Frequency</th>
            <th>Percentage</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let item of data.results">
            <td>{{ item.rank }}</td>
            <td class="word">{{ item.word }}</td>
            <td>{{ item.count }}</td>
            <td>
              <div class="bar-container">
                <div
                  class="bar"
                  [style.width.%]="(item.count / maxCount) * 100"
                ></div>
                <span class="percentage">{{ getPercentage(item.count) }}%</span>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
  styleUrl: './frequency-table.component.css'
})
export class FrequencyTableComponent {
  @Input() data: AnalyzeResponse | null = null;

  get maxCount(): number {
    if (!this.data || this.data.results.length === 0) return 1;
    return Math.max(...this.data.results.map(r => r.count));
  }

  getPercentage(count: number): number {
    if (!this.data) return 0;
    return Math.round((count / this.data.totalWords) * 100);
  }
}
