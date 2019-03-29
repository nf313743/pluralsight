import { AbstractControl, ValidatorFn, NG_VALIDATORS, Validator } from "@angular/forms";
import { Directive, forwardRef } from "@angular/core";

function notLowerCaseValidate(c: AbstractControl): ValidatorFn {
    let ret: any = null;

    const value = c.value;
    if (value) {
        if (value.toString().trim() === value.toString().toLowerCase().trim()) {
            ret = { validateNotlowercase: { value } };
        }
    }

    return ret;
}

@Directive({
    selector: '[validateNotlowercase]',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: forwardRef(() =>
            NotLowerCaseValidatorDirective),
        multi: true
    }]
})
export class NotLowerCaseValidatorDirective implements Validator {
    private validator: ValidatorFn;

    constructor() {
        this.validator = notLowerCaseValidate;
    }

    validate(c: AbstractControl): { [key: string]: any } {
        return this.validator(c);
    }
}