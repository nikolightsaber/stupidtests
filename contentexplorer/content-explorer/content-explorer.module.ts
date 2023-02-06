import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ContentExplorerComponent } from './content-explorer.component';

@NgModule({
    declarations: [
        ContentExplorerComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    exports: [ContentExplorerComponent],
})
export class ContentExplorerModule { }
