import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface WordCount {
  word: string;
  count: number;
  rank: number;
}

export interface AnalyzeResponse {
  id: string;
  totalWords: number;
  uniqueWords: number;
  results: WordCount[];
}

@Injectable({
  providedIn: 'root'
})
export class AnalysisService {
  private apiUrl = `${environment.apiUrl}/analysis`;

  constructor(private http: HttpClient) {}

  analyzeText(text: string): Observable<AnalyzeResponse> {
    return this.http.post<AnalyzeResponse>(this.apiUrl, { text });
  }
}
