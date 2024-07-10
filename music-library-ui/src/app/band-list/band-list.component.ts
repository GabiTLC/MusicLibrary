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
  openedAlbums: { bandIndex: number, albumIndex: number }[] = [];

  constructor(private bandService: BandService) { }

  ngOnInit(): void {
    this.bandService.getBands().subscribe(data => {
      this.bands = data;
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
}
