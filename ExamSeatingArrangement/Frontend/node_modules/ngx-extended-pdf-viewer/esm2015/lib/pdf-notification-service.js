/**
 * @fileoverview added by tsickle
 * Generated from: lib/pdf-notification-service.ts
 * @suppress {checkTypes,constantProperty,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Injectable } from '@angular/core';
import { Subject, ReplaySubject } from 'rxjs';
import * as i0 from "@angular/core";
export class PDFNotificationService {
    constructor() {
        // this event is fired when the pdf.js library has been loaded and objects like PDFApplication are available
        this.onPDFJSInit = new Subject();
        this.pdfjsVersion = new ReplaySubject(1);
        if (((/** @type {?} */ (window))).pdfjsLib && ((/** @type {?} */ (window))).pdfjsLib.version) {
            this.pdfjsVersion.next(((/** @type {?} */ (window))).pdfjsLib.version);
        }
        else {
            this.onPDFJSInit.subscribe((/**
             * @return {?}
             */
            () => {
                this.pdfjsVersion.next(((/** @type {?} */ (window))).pdfjsLib.version);
            }));
        }
    }
}
PDFNotificationService.decorators = [
    { type: Injectable, args: [{
                providedIn: 'root',
            },] }
];
/** @nocollapse */
PDFNotificationService.ctorParameters = () => [];
/** @nocollapse */ PDFNotificationService.ngInjectableDef = i0.defineInjectable({ factory: function PDFNotificationService_Factory() { return new PDFNotificationService(); }, token: PDFNotificationService, providedIn: "root" });
if (false) {
    /** @type {?} */
    PDFNotificationService.prototype.onPDFJSInit;
    /** @type {?} */
    PDFNotificationService.prototype.pdfjsVersion;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGRmLW5vdGlmaWNhdGlvbi1zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LWV4dGVuZGVkLXBkZi12aWV3ZXIvIiwic291cmNlcyI6WyJsaWIvcGRmLW5vdGlmaWNhdGlvbi1zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7O0FBQUEsT0FBTyxFQUFFLFVBQVUsRUFBRSxNQUFNLGVBQWUsQ0FBQztBQUMzQyxPQUFPLEVBQUUsT0FBTyxFQUFFLGFBQWEsRUFBRSxNQUFNLE1BQU0sQ0FBQzs7QUFLOUMsTUFBTSxPQUFPLHNCQUFzQjtJQU1qQzs7UUFKTyxnQkFBVyxHQUFHLElBQUksT0FBTyxFQUFRLENBQUM7UUFFbEMsaUJBQVksR0FBRyxJQUFJLGFBQWEsQ0FBUyxDQUFDLENBQUMsQ0FBQztRQUdqRCxJQUFJLENBQUMsbUJBQUEsTUFBTSxFQUFPLENBQUMsQ0FBQyxRQUFRLElBQUksQ0FBQyxtQkFBQSxNQUFNLEVBQU8sQ0FBQyxDQUFDLFFBQVEsQ0FBQyxPQUFPLEVBQUU7WUFDaEUsSUFBSSxDQUFDLFlBQVksQ0FBQyxJQUFJLENBQUMsQ0FBQyxtQkFBQSxNQUFNLEVBQU8sQ0FBQyxDQUFDLFFBQVEsQ0FBQyxPQUFPLENBQUMsQ0FBQztTQUMxRDthQUFNO1lBQ0wsSUFBSSxDQUFDLFdBQVcsQ0FBQyxTQUFTOzs7WUFBQyxHQUFHLEVBQUU7Z0JBQzlCLElBQUksQ0FBQyxZQUFZLENBQUMsSUFBSSxDQUFDLENBQUMsbUJBQUEsTUFBTSxFQUFPLENBQUMsQ0FBQyxRQUFRLENBQUMsT0FBTyxDQUFDLENBQUM7WUFDM0QsQ0FBQyxFQUFDLENBQUM7U0FDSjtJQUNILENBQUM7OztZQWpCRixVQUFVLFNBQUM7Z0JBQ1YsVUFBVSxFQUFFLE1BQU07YUFDbkI7Ozs7Ozs7SUFHQyw2Q0FBeUM7O0lBRXpDLDhDQUFtRCIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IEluamVjdGFibGUgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcbmltcG9ydCB7IFN1YmplY3QsIFJlcGxheVN1YmplY3QgfSBmcm9tICdyeGpzJztcblxuQEluamVjdGFibGUoe1xuICBwcm92aWRlZEluOiAncm9vdCcsXG59KVxuZXhwb3J0IGNsYXNzIFBERk5vdGlmaWNhdGlvblNlcnZpY2Uge1xuICAvLyB0aGlzIGV2ZW50IGlzIGZpcmVkIHdoZW4gdGhlIHBkZi5qcyBsaWJyYXJ5IGhhcyBiZWVuIGxvYWRlZCBhbmQgb2JqZWN0cyBsaWtlIFBERkFwcGxpY2F0aW9uIGFyZSBhdmFpbGFibGVcbiAgcHVibGljIG9uUERGSlNJbml0ID0gbmV3IFN1YmplY3Q8dm9pZD4oKTtcblxuICBwdWJsaWMgcGRmanNWZXJzaW9uID0gbmV3IFJlcGxheVN1YmplY3Q8c3RyaW5nPigxKTtcblxuICBjb25zdHJ1Y3RvcigpIHtcbiAgICBpZiAoKHdpbmRvdyBhcyBhbnkpLnBkZmpzTGliICYmICh3aW5kb3cgYXMgYW55KS5wZGZqc0xpYi52ZXJzaW9uKSB7XG4gICAgICB0aGlzLnBkZmpzVmVyc2lvbi5uZXh0KCh3aW5kb3cgYXMgYW55KS5wZGZqc0xpYi52ZXJzaW9uKTtcbiAgICB9IGVsc2Uge1xuICAgICAgdGhpcy5vblBERkpTSW5pdC5zdWJzY3JpYmUoKCkgPT4ge1xuICAgICAgICB0aGlzLnBkZmpzVmVyc2lvbi5uZXh0KCh3aW5kb3cgYXMgYW55KS5wZGZqc0xpYi52ZXJzaW9uKTtcbiAgICAgIH0pO1xuICAgIH1cbiAgfVxufVxuIl19