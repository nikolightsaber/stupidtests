import { Component, Input } from '@angular/core';

@Component({
    selector: 'content-explorer',
    templateUrl: 'content-explorer.view.html',
    styleUrls: ['content-explorer.scss'],
})
export class ContentExplorerComponent
{
    @Input() set Data(data: any)
    {
        this.parseContent(data);
    }

    @Input() Level = 0;

    public Content: any;
    public Value: any;

    isObject = (val: any) => val && typeof val === 'object' && !Array.isArray(val);

    parseContent(data: any)
    {
        if (!data)
        {
            return;
        }

        if (this.isObject(data))
        {
            this.Level++;
            this.Content = data;
            return;
        }

        // Here add filtering method to check value and reformat or change it

        this.Value = data;
    }
}
