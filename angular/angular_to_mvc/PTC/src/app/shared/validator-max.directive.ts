import { AbstractControl, ValidatorFn, NG_VALIDATORS, Validator } from "@angular/forms";
import { Directive, forwardRef, OnInit, Input } from "@angular/core";

export const max = (max: number): ValidatorFn => {
    return (c: AbstractControl): { [key: string]: boolean } => {
        let ret: any = null;

        if (max !== undefined && max !== null) {
            let value: number = +c.value;
            if (value > +max) {
                ret = { max: true };
            }
        }
        return ret;
    };
};

@Directive({
    selector: '[max]',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: forwardRef(() =>
            MaxValidatorDirective),
        multi: true
    }]
})
export class MaxValidatorDirective implements Validator, OnInit {
    @Input() max: number;

    private validator: ValidatorFn;

    ngOnInit() {
        this.validator = max(this.max)
    }

    validate(c: AbstractControl): { [key: string]: any } {
        return this.validator(c);
    }
}