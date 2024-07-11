import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Song {
  id: string;
  title: string;
  length: string;
}

interface Album {
  id: string;
  title: string;
  description: string;
  songs: Song[];
}

interface Band {
  id: string;
  name: string;
  albums: Album[];
}

@Injectable({
  providedIn: 'root'
})
export class BandService {
  private apiUrl = '/api/bands'; //backend endpoint

  constructor(private http: HttpClient) { }

  getBands(): Observable<Band[]> {
    return this.http.get<Band[]>(this.apiUrl);
  }
}
