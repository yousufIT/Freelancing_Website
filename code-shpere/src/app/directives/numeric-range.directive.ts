// src/app/directives/numeric-range.directive.ts
import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appNumericRange]',
  standalone: true
})
export class NumericRangeDirective {
  // Allow to set min/max via attribute binding [min] and [max] like normal input attributes
  @Input() min?: number | string;
  @Input() max?: number | string;

  private el: HTMLInputElement;

  constructor(private elementRef: ElementRef<HTMLInputElement>) {
    this.el = this.elementRef.nativeElement;
  }

  // Prevent invalid keys: e, E, +, - (minus), and prevent multiple dots
  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    const allowedControlKeys = [
      'Backspace', 'Tab', 'ArrowLeft', 'ArrowRight', 'Delete', 'Home', 'End'
    ];

    if (allowedControlKeys.includes(event.key)) {
      return;
    }

    // allow decimal separator if input[type=number] and step allows decimals
    const isDot = event.key === '.' || event.key === ',';

    // block exponent and signs
    if (event.key.toLowerCase() === 'e' || event.key === '+' || event.key === '-') {
      event.preventDefault();
      return;
    }

    // Allow digits and dot only
    const isNumber = /^[0-9]$/.test(event.key);
    if (!isNumber && !isDot) {
      event.preventDefault();
      return;
    }

    // prevent second dot
    if (isDot && (this.el.value.includes('.') || this.el.value.includes(','))) {
      event.preventDefault();
      return;
    }
  }

  // Prevent mouse wheel from changing the value (common unwanted behaviour)
  @HostListener('wheel', ['$event'])
  onWheel(event: WheelEvent) {
    // if the element is focused, prevent wheel changing value
    if (document.activeElement === this.el) {
      event.preventDefault();
    }
  }

  // sanitize paste (remove non-number characters and clamp)
  @HostListener('paste', ['$event'])
  onPaste(event: ClipboardEvent) {
    event.preventDefault();
    const text = event.clipboardData?.getData('text') ?? '';
    const sanitized = text.replace(/[^0-9.,-]/g, '').replace(',', '.');
    // if negative sign present, remove it (we want positive)
    const positive = sanitized.replace(/-/g, '');
    const num = parseFloat(positive);
    if (!isNaN(num)) {
      this.el.value = positive;
      this.clampAndFormat();
      this.el.dispatchEvent(new Event('input'));
    }
  }

  // on input: sanitize stray characters (useful for mobile keyboards)
  @HostListener('input')
  onInput() {
    // remove any letters except digits and dot and comma and minus (minus will be removed later)
    let v = this.el.value;
    v = v.replace(/,/g, '.'); // unify decimal separator
    // remove any character that isn't digit or dot or minus
    v = v.replace(/[^0-9.-]/g, '');

    // remove minus signs anywhere
    v = v.replace(/-/g, '');

    // ensure only one dot
    const parts = v.split('.');
    if (parts.length > 2) {
      v = parts[0] + '.' + parts.slice(1).join('');
    }

    if (v !== this.el.value) {
      this.el.value = v;
      this.el.dispatchEvent(new Event('input'));
    }
  }

  // on blur: clamp value to min/max and normalize (remove trailing dot)
  @HostListener('blur')
  clampAndFormat() {
    if (!this.el.value) return;

    let value = parseFloat(this.el.value);
    if (isNaN(value)) {
      this.el.value = '';
      this.el.dispatchEvent(new Event('input'));
      return;
    }

    const min = this.parseInputNumber(this.min);
    const max = this.parseInputNumber(this.max);

    if (min !== undefined && !isNaN(min) && value < min) {
      value = min;
    }
    if (max !== undefined && !isNaN(max) && value > max) {
      value = max;
    }

    // if step is integer (no dot in step attribute) and max-min are integers, round to integer?
    // We'll keep precision as-is; you can format as needed.
    // Remove trailing decimal if it's integer
    if (Number.isInteger(value)) {
      this.el.value = value.toString();
    } else {
      // limit to two decimals by default to keep it nice
      this.el.value = parseFloat(value.toFixed(2)).toString();
    }

    // dispatch input so Angular picks up change
    this.el.dispatchEvent(new Event('input'));
  }

  private parseInputNumber(val?: number | string): number | undefined {
    if (val === undefined || val === null || val === '') return undefined;
    const n = typeof val === 'string' ? parseFloat(val) : val;
    return isNaN(n) ? undefined : n;
  }
}
