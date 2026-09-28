import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../Modal/modal.component';


@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  constructor(private dialog: MatDialog) { }

  openExportModal(): void {
    this.dialog.open(ModalComponent, {
      width: '400px',
      data: { title: 'Export Expenses' }
    });

  }
}
