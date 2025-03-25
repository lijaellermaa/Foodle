import {Component, Input, OnInit} from '@angular/core';
import {NgStyle} from "@angular/common";

@Component({
  selector: 'input-container',
  standalone: true,
  templateUrl: './input-container.component.html',
  imports: [
    NgStyle
  ],
  styleUrls: ['./input-container.component.css']
})
export class InputContainerComponent implements OnInit {
  @Input()
  label!: string;
  @Input()
  bgColor = 'white';

  constructor() {
  }

  ngOnInit(): void {

  }
}
