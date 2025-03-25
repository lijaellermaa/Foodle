import {Component, Input, OnInit} from '@angular/core';
import {NgStyle} from "@angular/common";

@Component({
  selector: 'app-title',
  standalone: true,
  templateUrl: './title.component.html',
  imports: [
    NgStyle
  ],
  styleUrls: ['./title.component.css']
})
export class TitleComponent implements OnInit {
  @Input()
  title!: string;
  @Input()
  margin? = '1rem 0 1rem 0.2rem';
  @Input()
  fontSize? = '1.7rem';

  constructor() {
  }

  ngOnInit(): void {
  }
}
