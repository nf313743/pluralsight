import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConvertToSpacesPipe } from './convert-to-spaces.pipe';
import { StarComponent } from './start.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        ConvertToSpacesPipe,
        StarComponent,
    ],
    exports: [
        ConvertToSpacesPipe,
        StarComponent,
        CommonModule,
        FormsModule
    ]
})
export class SharedModule { }
