import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BandService } from '../band.service';

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

@Component({
  selector: 'app-band-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './band-list.component.html',
  styleUrls: ['./band-list.component.css']
})
export class BandListComponent implements OnInit {
  bands: Band[] = [];
  filteredBands: Band[] = [];
  openedAlbums: { bandIndex: number, albumIndex: number }[] = [];
  searchQuery: string = '';

  constructor(private bandService: BandService) { }

  ngOnInit(): void {
    this.bandService.getBands().subscribe(data => {
      this.bands = data;
      this.filteredBands = data;
    });
  }

  toggleAlbum(bandIndex: number, albumIndex: number): void {
    const index = this.openedAlbums.findIndex(album => album.bandIndex === bandIndex && album.albumIndex === albumIndex);
    if (index > -1) {
      this.openedAlbums.splice(index, 1);
    } else {
      this.openedAlbums.push({ bandIndex, albumIndex });
    }
  }

  isAlbumOpened(bandIndex: number, albumIndex: number): boolean {
    return this.openedAlbums.some(album => album.bandIndex === bandIndex && album.albumIndex === albumIndex);
  }

  getRowStart(bandIndex: number): number {
    return Math.floor(bandIndex / 3) * 3;
  }

  getRowEnd(bandIndex: number): number {
    return Math.min(this.getRowStart(bandIndex) + 3, this.filteredBands.length);
  }

  isAnyAlbumOpenedInRow(bandIndex: number): boolean {
    const start = this.getRowStart(bandIndex);
    const end = this.getRowEnd(bandIndex);
    return this.openedAlbums.some(album => album.bandIndex >= start && album.bandIndex < end);
  }

  onSearch(event: Event): void {
    const query = (event.target as HTMLInputElement).value.toLowerCase();
    this.filteredBands = this.bands.filter(band =>
      band.name.toLowerCase().includes(query) ||
      band.albums.some(album =>
        album.title.toLowerCase().includes(query) ||
        album.songs.some(song => song.title.toLowerCase().includes(query))
      )
    );
  }
}
