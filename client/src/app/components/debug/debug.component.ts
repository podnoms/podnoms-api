import { DropzoneConfigInterface } from 'app/components/shared/dropzone/dropzone.interfaces';
import { Observable } from 'rxjs/Observable';
import { environment } from 'environments/environment';
import { SignalRService } from 'app/services/signalr.service';
import { HubConnection } from '@aspnet/signalr-client';
import { Component, OnInit } from '@angular/core';
import { DebugService } from 'app/services/debug.service';

@Component({
    selector: 'app-debug',
    templateUrl: './debug.component.html',
    styleUrls: ['./debug.component.css']
})
export class DebugComponent implements OnInit {
    realtimeMessage: string;
    messagesReceived: string[] = [];

    debugInfo$: Observable<string>;

    config: DropzoneConfigInterface = {
        acceptedFiles: 'audio/*',
        maxFilesize: 4000, // 4Gb
        timeout: 1000 * (60 * 120), /// 2 hours
        maxFiles: 1
    };
    constructor(private _debugService: DebugService, private _signalRService: SignalRService) {}
    ngOnInit() {
        this._signalRService.init(`${environment.signalRHost}hubs/debug`);
        this._signalRService.connection.on('Send', data => {
            console.log('DebugService', 'signalr', data);
            this.messagesReceived.push(data);
            this.realtimeMessage = '';
        });
        this.debugInfo$ = this._debugService.getDebugInfo();
    }

    sendMessage() {
        this._debugService.sendRealtime(this.realtimeMessage).subscribe(r => console.log(r));
    }
    doSomething() {
        alert('doSomething was did');
    }
}
