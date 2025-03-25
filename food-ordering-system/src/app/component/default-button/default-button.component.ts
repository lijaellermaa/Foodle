import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgStyle} from "@angular/common";

@Component({
  selector: 'default-button',
  standalone: true,
  imports: [
    NgStyle
  ],
  templateUrl: './default-button.component.html',
  styleUrl: './default-button.component.css'
})
export class DefaultButtonComponent implements OnInit {
  @Input()
  type: 'submit' | 'button' = 'submit';
  @Input()
  text: string = 'Submit';
  @Input()
  bgColor = '#ff8c00';
  @Input()
  color = 'white';
  @Input()
  fontSizeRem = 1.3;
  @Input()
  widthRem = 12;
  @Output()
  onClick = new EventEmitter();

  constructor() {
  }

  ngOnInit(): void {
  }
}
