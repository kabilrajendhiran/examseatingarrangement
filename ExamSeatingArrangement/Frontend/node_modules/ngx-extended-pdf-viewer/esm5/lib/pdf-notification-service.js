/**
 * @fileoverview added by tsickle
 * Generated from: lib/pdf-notification-service.ts
 * @suppress {checkTypes,constantProperty,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Injectable } from '@angular/core';
import { Subject, ReplaySubject } from 'rxjs';
import * as i0 from "@angular/core";
var PDFNotificationService = /** @class */ (function () {
    function PDFNotificationService() {
        var _this = this;
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
            function () {
                _this.pdfjsVersion.next(((/** @type {?} */ (window))).pdfjsLib.version);
            }));
        }
    }
    PDFNotificationService.decorators = [
        { type: Injectable, args: [{
                    providedIn: 'root',
                },] }
    ];
    /** @nocollapse */
    PDFNotificationService.ctorParameters = function () { return []; };
    /** @nocollapse */ PDFNotificationService.ngInjectableDef = i0.defineInjectable({ factory: function PDFNotificationService_Factory() { return new PDFNotificationService(); }, token: PDFNotificationService, providedIn: "root" });
    return PDFNotificationService;
}());
export { PDFNotificationService };
if (false) {
    /** @type {?} */
    PDFNotificationService.prototype.onPDFJSInit;
    /** @type {?} */
    PDFNotificationService.prototype.pdfjsVersion;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGRmLW5vdGlmaWNhdGlvbi1zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LWV4dGVuZGVkLXBkZi12aWV3ZXIvIiwic291cmNlcyI6WyJsaWIvcGRmLW5vdGlmaWNhdGlvbi1zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7O0FBQUEsT0FBTyxFQUFFLFVBQVUsRUFBRSxNQUFNLGVBQWUsQ0FBQztBQUMzQyxPQUFPLEVBQUUsT0FBTyxFQUFFLGFBQWEsRUFBRSxNQUFNLE1BQU0sQ0FBQzs7QUFFOUM7SUFTRTtRQUFBLGlCQVFDOztRQVpNLGdCQUFXLEdBQUcsSUFBSSxPQUFPLEVBQVEsQ0FBQztRQUVsQyxpQkFBWSxHQUFHLElBQUksYUFBYSxDQUFTLENBQUMsQ0FBQyxDQUFDO1FBR2pELElBQUksQ0FBQyxtQkFBQSxNQUFNLEVBQU8sQ0FBQyxDQUFDLFFBQVEsSUFBSSxDQUFDLG1CQUFBLE1BQU0sRUFBTyxDQUFDLENBQUMsUUFBUSxDQUFDLE9BQU8sRUFBRTtZQUNoRSxJQUFJLENBQUMsWUFBWSxDQUFDLElBQUksQ0FBQyxDQUFDLG1CQUFBLE1BQU0sRUFBTyxDQUFDLENBQUMsUUFBUSxDQUFDLE9BQU8sQ0FBQyxDQUFDO1NBQzFEO2FBQU07WUFDTCxJQUFJLENBQUMsV0FBVyxDQUFDLFNBQVM7OztZQUFDO2dCQUN6QixLQUFJLENBQUMsWUFBWSxDQUFDLElBQUksQ0FBQyxDQUFDLG1CQUFBLE1BQU0sRUFBTyxDQUFDLENBQUMsUUFBUSxDQUFDLE9BQU8sQ0FBQyxDQUFDO1lBQzNELENBQUMsRUFBQyxDQUFDO1NBQ0o7SUFDSCxDQUFDOztnQkFqQkYsVUFBVSxTQUFDO29CQUNWLFVBQVUsRUFBRSxNQUFNO2lCQUNuQjs7Ozs7aUNBTEQ7Q0FxQkMsQUFsQkQsSUFrQkM7U0FmWSxzQkFBc0I7OztJQUVqQyw2Q0FBeUM7O0lBRXpDLDhDQUFtRCIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IEluamVjdGFibGUgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcbmltcG9ydCB7IFN1YmplY3QsIFJlcGxheVN1YmplY3QgfSBmcm9tICdyeGpzJztcblxuQEluamVjdGFibGUoe1xuICBwcm92aWRlZEluOiAncm9vdCcsXG59KVxuZXhwb3J0IGNsYXNzIFBERk5vdGlmaWNhdGlvblNlcnZpY2Uge1xuICAvLyB0aGlzIGV2ZW50IGlzIGZpcmVkIHdoZW4gdGhlIHBkZi5qcyBsaWJyYXJ5IGhhcyBiZWVuIGxvYWRlZCBhbmQgb2JqZWN0cyBsaWtlIFBERkFwcGxpY2F0aW9uIGFyZSBhdmFpbGFibGVcbiAgcHVibGljIG9uUERGSlNJbml0ID0gbmV3IFN1YmplY3Q8dm9pZD4oKTtcblxuICBwdWJsaWMgcGRmanNWZXJzaW9uID0gbmV3IFJlcGxheVN1YmplY3Q8c3RyaW5nPigxKTtcblxuICBjb25zdHJ1Y3RvcigpIHtcbiAgICBpZiAoKHdpbmRvdyBhcyBhbnkpLnBkZmpzTGliICYmICh3aW5kb3cgYXMgYW55KS5wZGZqc0xpYi52ZXJzaW9uKSB7XG4gICAgICB0aGlzLnBkZmpzVmVyc2lvbi5uZXh0KCh3aW5kb3cgYXMgYW55KS5wZGZqc0xpYi52ZXJzaW9uKTtcbiAgICB9IGVsc2Uge1xuICAgICAgdGhpcy5vblBERkpTSW5pdC5zdWJzY3JpYmUoKCkgPT4ge1xuICAgICAgICB0aGlzLnBkZmpzVmVyc2lvbi5uZXh0KCh3aW5kb3cgYXMgYW55KS5wZGZqc0xpYi52ZXJzaW9uKTtcbiAgICAgIH0pO1xuICAgIH1cbiAgfVxufVxuIl19