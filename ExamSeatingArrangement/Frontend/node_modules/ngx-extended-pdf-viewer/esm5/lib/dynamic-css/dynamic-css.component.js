/**
 * @fileoverview added by tsickle
 * Generated from: lib/dynamic-css/dynamic-css.component.ts
 * @suppress {checkTypes,constantProperty,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Component, Renderer2, Inject, Input } from '@angular/core';
import { DOCUMENT } from '@angular/common';
var DynamicCssComponent = /** @class */ (function () {
    function DynamicCssComponent(renderer, document) {
        this.renderer = renderer;
        this.document = document;
        this.zoom = 1.0;
        this.width = 100;
        this.xs = 490;
        this.sm = 560;
        this.md = 610;
        this.lg = 660;
        this.xl = 740;
        this.xxl = 830;
    }
    Object.defineProperty(DynamicCssComponent.prototype, "style", {
        get: /**
         * @return {?}
         */
        function () {
            return "\n@media all and (max-width: " + this.xl + "px) {\n  #toolbarViewerMiddle {\n    display: table;\n    margin: auto;\n    left: auto;\n    position: inherit;\n    transform: none;\n  }\n}\n\n@media all and (max-width: 840px) {\n  #sidebarContent {\n    background-color: rgba(0, 0, 0, 0.7);\n  }\n\n  html[dir='ltr'] #outerContainer.sidebarOpen #viewerContainer {\n    left: 0px !important;\n  }\n  html[dir='rtl'] #outerContainer.sidebarOpen #viewerContainer {\n    right: 0px !important;\n  }\n\n  #outerContainer .hiddenLargeView,\n  #outerContainer .hiddenMediumView {\n    display: inherit;\n  }\n  #outerContainer .visibleLargeView,\n  #outerContainer .visibleMediumView {\n    display: none;\n  }\n}\n\n@media all and (max-width: " + this.lg + "px) {\n  .toolbarButtonSpacer {\n    width: 15px;\n  }\n\n  #outerContainer .hiddenLargeView {\n    display: none;\n  }\n  #outerContainer .visibleLargeView {\n    display: inherit;\n  }\n}\n\n@media all and (max-width: " + this.md + "px) {\n  .toolbarButtonSpacer {\n    display: none;\n  }\n  #outerContainer .hiddenMediumView {\n    display: none;\n  }\n  #outerContainer .visibleMediumView {\n    display: inherit;\n  }\n}\n\n@media all and (max-width: " + this.sm + "px) {\n  .hiddenSmallView,\n  .hiddenSmallView * {\n    display: none;\n  }\n  .visibleSmallView {\n    display: inherit;\n  }\n  .toolbarButtonSpacer {\n    width: 0;\n  }\n  html[dir='ltr'] .findbar {\n    left: 38px;\n  }\n  html[dir='rtl'] .findbar {\n    right: 38px;\n  }\n}\n\n@media all and (max-width: " + this.xs + "px) {\n  #scaleSelectContainer {\n    display: none;\n  }\n}\n\n.visibleXLView,\n.visibleXXLView,\n.visibleTinyView {\n  display: none;\n}\n\n.hiddenXLView,\n.hiddenXXLView {\n  display: unset;\n}\n\n@media all and (max-width: " + this.xl + "px) {\n  #outerContainer .hiddenXLView {\n    display: none;\n  }\n  #outerContainer .visibleXLView {\n    display: inherit;\n  }\n\n  #toolbarViewerMiddle {\n    -webkit-transform: translateX(-36%);\n    transform: translateX(-36%);\n    display: unset;\n    margin: unset;\n    left: 50%;\n    position: absolute;\n  }\n}\n\n@media all and (max-width: " + this.xxl + "px) {\n  #outerContainer .hiddenXXLView {\n    display: none;\n  }\n  #outerContainer .visibleXXLView {\n    display: inherit;\n  }\n}\n\n@media all and (max-width: " + this.md + "px) {\n  #toolbarViewerMiddle {\n    -webkit-transform: translateX(-26%);\n    transform: translateX(-26%);\n  }\n}\n\n@media all and (max-width: " + this.xs + "px) {\n  .hiddenTinyView,\n  .hiddenTinyView * {\n    display: none;\n  }\n  .visibleTinyView {\n    display: inherit;\n  }\n}\n  ";
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @return {?}
     */
    DynamicCssComponent.prototype.ngOnInit = /**
     * @return {?}
     */
    function () {
        this.injectStyle();
    };
    /**
     * @return {?}
     */
    DynamicCssComponent.prototype.ngOnChanges = /**
     * @return {?}
     */
    function () {
        /** @type {?} */
        var fullWith = this.document.body.clientWidth;
        /** @type {?} */
        var partialViewScale = fullWith / this.width;
        /** @type {?} */
        var scaleFactor = partialViewScale * (this.zoom ? this.zoom : 1);
        this.xs = scaleFactor * 490;
        this.sm = scaleFactor * 560;
        this.md = scaleFactor * 610;
        this.lg = scaleFactor * 660;
        this.xl = scaleFactor * 740;
        this.xxl = scaleFactor * 830;
        /** @type {?} */
        var styles = this.document.getElementById('pdf-dynamic-css');
        if (styles) {
            styles.innerHTML = this.style;
        }
    };
    /**
     * @private
     * @return {?}
     */
    DynamicCssComponent.prototype.injectStyle = /**
     * @private
     * @return {?}
     */
    function () {
        /** @type {?} */
        var styles = (/** @type {?} */ (this.document.createElement('STYLE')));
        styles.id = 'pdf-dynamic-css';
        styles.innerHTML = this.style;
        this.renderer.appendChild(this.document.head, styles);
    };
    /**
     * @return {?}
     */
    DynamicCssComponent.prototype.ngOnDestroy = /**
     * @return {?}
     */
    function () {
        /** @type {?} */
        var styles = (/** @type {?} */ (this.document.getElementById('pdf-dynamic-css')));
        if (styles && styles.parentElement) {
            ((/** @type {?} */ (styles.parentElement))).removeChild(styles);
        }
    };
    DynamicCssComponent.decorators = [
        { type: Component, args: [{
                    selector: 'pdf-dynamic-css',
                    template: "",
                    styles: [""]
                }] }
    ];
    /** @nocollapse */
    DynamicCssComponent.ctorParameters = function () { return [
        { type: Renderer2 },
        { type: undefined, decorators: [{ type: Inject, args: [DOCUMENT,] }] }
    ]; };
    DynamicCssComponent.propDecorators = {
        zoom: [{ type: Input }],
        width: [{ type: Input }]
    };
    return DynamicCssComponent;
}());
export { DynamicCssComponent };
if (false) {
    /** @type {?} */
    DynamicCssComponent.prototype.zoom;
    /** @type {?} */
    DynamicCssComponent.prototype.width;
    /** @type {?} */
    DynamicCssComponent.prototype.xs;
    /** @type {?} */
    DynamicCssComponent.prototype.sm;
    /** @type {?} */
    DynamicCssComponent.prototype.md;
    /** @type {?} */
    DynamicCssComponent.prototype.lg;
    /** @type {?} */
    DynamicCssComponent.prototype.xl;
    /** @type {?} */
    DynamicCssComponent.prototype.xxl;
    /**
     * @type {?}
     * @private
     */
    DynamicCssComponent.prototype.renderer;
    /**
     * @type {?}
     * @private
     */
    DynamicCssComponent.prototype.document;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZHluYW1pYy1jc3MuY29tcG9uZW50LmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LWV4dGVuZGVkLXBkZi12aWV3ZXIvIiwic291cmNlcyI6WyJsaWIvZHluYW1pYy1jc3MvZHluYW1pYy1jc3MuY29tcG9uZW50LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7O0FBQUEsT0FBTyxFQUFFLFNBQVMsRUFBVSxTQUFTLEVBQUUsTUFBTSxFQUFhLEtBQUssRUFBYSxNQUFNLGVBQWUsQ0FBQztBQUNsRyxPQUFPLEVBQUUsUUFBUSxFQUFFLE1BQU0saUJBQWlCLENBQUM7QUFFM0M7SUFxS0UsNkJBQW9CLFFBQW1CLEVBQTRCLFFBQWE7UUFBNUQsYUFBUSxHQUFSLFFBQVEsQ0FBVztRQUE0QixhQUFRLEdBQVIsUUFBUSxDQUFLO1FBOUp6RSxTQUFJLEdBQUcsR0FBRyxDQUFDO1FBR1gsVUFBSyxHQUFHLEdBQUcsQ0FBQztRQUVaLE9BQUUsR0FBRyxHQUFHLENBQUM7UUFFVCxPQUFFLEdBQUcsR0FBRyxDQUFDO1FBRVQsT0FBRSxHQUFHLEdBQUcsQ0FBQztRQUVULE9BQUUsR0FBRyxHQUFHLENBQUM7UUFFVCxPQUFFLEdBQUcsR0FBRyxDQUFDO1FBRVQsUUFBRyxHQUFHLEdBQUcsQ0FBQztJQStJa0UsQ0FBQztJQTdJcEYsc0JBQVcsc0NBQUs7Ozs7UUFBaEI7WUFDRSxPQUFPLGtDQUNrQixJQUFJLENBQUMsRUFBRSw0ckJBZ0NQLElBQUksQ0FBQyxFQUFFLG9PQWFQLElBQUksQ0FBQyxFQUFFLHNPQVlQLElBQUksQ0FBQyxFQUFFLCtUQW1CUCxJQUFJLENBQUMsRUFBRSwyT0FpQlAsSUFBSSxDQUFDLEVBQUUsMFdBa0JQLElBQUksQ0FBQyxHQUFHLDZLQVNSLElBQUksQ0FBQyxFQUFFLDBKQU9QLElBQUksQ0FBQyxFQUFFLHVJQVNqQyxDQUFDO1FBQ0osQ0FBQzs7O09BQUE7Ozs7SUFJQyxzQ0FBUTs7O0lBQVI7UUFDRSxJQUFJLENBQUMsV0FBVyxFQUFFLENBQUM7SUFDckIsQ0FBQzs7OztJQUVELHlDQUFXOzs7SUFBWDs7WUFDUSxRQUFRLEdBQUcsSUFBSSxDQUFDLFFBQVEsQ0FBQyxJQUFJLENBQUMsV0FBVzs7WUFDekMsZ0JBQWdCLEdBQUcsUUFBUSxHQUFHLElBQUksQ0FBQyxLQUFLOztZQUN4QyxXQUFXLEdBQUcsZ0JBQWdCLEdBQUcsQ0FBQyxJQUFJLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FBQyxJQUFJLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUM7UUFFbEUsSUFBSSxDQUFDLEVBQUUsR0FBRyxXQUFXLEdBQUcsR0FBRyxDQUFDO1FBQzVCLElBQUksQ0FBQyxFQUFFLEdBQUcsV0FBVyxHQUFHLEdBQUcsQ0FBQztRQUM1QixJQUFJLENBQUMsRUFBRSxHQUFHLFdBQVcsR0FBRyxHQUFHLENBQUM7UUFDNUIsSUFBSSxDQUFDLEVBQUUsR0FBRyxXQUFXLEdBQUcsR0FBRyxDQUFDO1FBQzVCLElBQUksQ0FBQyxFQUFFLEdBQUcsV0FBVyxHQUFHLEdBQUcsQ0FBQztRQUM1QixJQUFJLENBQUMsR0FBRyxHQUFHLFdBQVcsR0FBRyxHQUFHLENBQUM7O1lBRXZCLE1BQU0sR0FBRyxJQUFJLENBQUMsUUFBUSxDQUFDLGNBQWMsQ0FBQyxpQkFBaUIsQ0FBQztRQUM5RCxJQUFJLE1BQU0sRUFBRTtZQUNWLE1BQU0sQ0FBQyxTQUFTLEdBQUcsSUFBSSxDQUFDLEtBQUssQ0FBQztTQUMvQjtJQUNILENBQUM7Ozs7O0lBRU8seUNBQVc7Ozs7SUFBbkI7O1lBQ1EsTUFBTSxHQUFHLG1CQUFBLElBQUksQ0FBQyxRQUFRLENBQUMsYUFBYSxDQUFDLE9BQU8sQ0FBQyxFQUFvQjtRQUN2RSxNQUFNLENBQUMsRUFBRSxHQUFHLGlCQUFpQixDQUFDO1FBQzlCLE1BQU0sQ0FBQyxTQUFTLEdBQUcsSUFBSSxDQUFDLEtBQUssQ0FBQztRQUM5QixJQUFJLENBQUMsUUFBUSxDQUFDLFdBQVcsQ0FBQyxJQUFJLENBQUMsUUFBUSxDQUFDLElBQUksRUFBRSxNQUFNLENBQUMsQ0FBQztJQUN4RCxDQUFDOzs7O0lBRU0seUNBQVc7OztJQUFsQjs7WUFDUSxNQUFNLEdBQUcsbUJBQUEsSUFBSSxDQUFDLFFBQVEsQ0FBQyxjQUFjLENBQUMsaUJBQWlCLENBQUMsRUFBZTtRQUM3RSxJQUFJLE1BQU0sSUFBSSxNQUFNLENBQUMsYUFBYSxFQUFFO1lBQ2xDLENBQUMsbUJBQUEsTUFBTSxDQUFDLGFBQWEsRUFBTyxDQUFDLENBQUMsV0FBVyxDQUFDLE1BQU0sQ0FBQyxDQUFDO1NBQ25EO0lBQ0gsQ0FBQzs7Z0JBek1GLFNBQVMsU0FBQztvQkFDVCxRQUFRLEVBQUUsaUJBQWlCO29CQUMzQixZQUEyQzs7aUJBRTVDOzs7O2dCQVAyQixTQUFTO2dEQXdLTyxNQUFNLFNBQUMsUUFBUTs7O3VCQS9KeEQsS0FBSzt3QkFHTCxLQUFLOztJQWlNUiwwQkFBQztDQUFBLEFBMU1ELElBME1DO1NBck1ZLG1CQUFtQjs7O0lBQzlCLG1DQUNrQjs7SUFFbEIsb0NBQ21COztJQUVuQixpQ0FBZ0I7O0lBRWhCLGlDQUFnQjs7SUFFaEIsaUNBQWdCOztJQUVoQixpQ0FBZ0I7O0lBRWhCLGlDQUFnQjs7SUFFaEIsa0NBQWlCOzs7OztJQStJTCx1Q0FBMkI7Ozs7O0lBQUUsdUNBQXVDIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgQ29tcG9uZW50LCBPbkluaXQsIFJlbmRlcmVyMiwgSW5qZWN0LCBPbkNoYW5nZXMsIElucHV0LCBPbkRlc3Ryb3kgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcbmltcG9ydCB7IERPQ1VNRU5UIH0gZnJvbSAnQGFuZ3VsYXIvY29tbW9uJztcblxuQENvbXBvbmVudCh7XG4gIHNlbGVjdG9yOiAncGRmLWR5bmFtaWMtY3NzJyxcbiAgdGVtcGxhdGVVcmw6ICcuL2R5bmFtaWMtY3NzLmNvbXBvbmVudC5odG1sJyxcbiAgc3R5bGVVcmxzOiBbJy4vZHluYW1pYy1jc3MuY29tcG9uZW50LmNzcyddXG59KVxuZXhwb3J0IGNsYXNzIER5bmFtaWNDc3NDb21wb25lbnQgaW1wbGVtZW50cyBPbkluaXQsIE9uQ2hhbmdlcywgT25EZXN0cm95IHtcbiAgQElucHV0KClcbiAgcHVibGljIHpvb20gPSAxLjA7XG5cbiAgQElucHV0KClcbiAgcHVibGljIHdpZHRoID0gMTAwO1xuXG4gIHB1YmxpYyB4cyA9IDQ5MDtcblxuICBwdWJsaWMgc20gPSA1NjA7XG5cbiAgcHVibGljIG1kID0gNjEwO1xuXG4gIHB1YmxpYyBsZyA9IDY2MDtcblxuICBwdWJsaWMgeGwgPSA3NDA7XG5cbiAgcHVibGljIHh4bCA9IDgzMDtcblxuICBwdWJsaWMgZ2V0IHN0eWxlKCk6IHN0cmluZyB7XG4gICAgcmV0dXJuIGBcbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy54bH1weCkge1xuICAjdG9vbGJhclZpZXdlck1pZGRsZSB7XG4gICAgZGlzcGxheTogdGFibGU7XG4gICAgbWFyZ2luOiBhdXRvO1xuICAgIGxlZnQ6IGF1dG87XG4gICAgcG9zaXRpb246IGluaGVyaXQ7XG4gICAgdHJhbnNmb3JtOiBub25lO1xuICB9XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6IDg0MHB4KSB7XG4gICNzaWRlYmFyQ29udGVudCB7XG4gICAgYmFja2dyb3VuZC1jb2xvcjogcmdiYSgwLCAwLCAwLCAwLjcpO1xuICB9XG5cbiAgaHRtbFtkaXI9J2x0ciddICNvdXRlckNvbnRhaW5lci5zaWRlYmFyT3BlbiAjdmlld2VyQ29udGFpbmVyIHtcbiAgICBsZWZ0OiAwcHggIWltcG9ydGFudDtcbiAgfVxuICBodG1sW2Rpcj0ncnRsJ10gI291dGVyQ29udGFpbmVyLnNpZGViYXJPcGVuICN2aWV3ZXJDb250YWluZXIge1xuICAgIHJpZ2h0OiAwcHggIWltcG9ydGFudDtcbiAgfVxuXG4gICNvdXRlckNvbnRhaW5lciAuaGlkZGVuTGFyZ2VWaWV3LFxuICAjb3V0ZXJDb250YWluZXIgLmhpZGRlbk1lZGl1bVZpZXcge1xuICAgIGRpc3BsYXk6IGluaGVyaXQ7XG4gIH1cbiAgI291dGVyQ29udGFpbmVyIC52aXNpYmxlTGFyZ2VWaWV3LFxuICAjb3V0ZXJDb250YWluZXIgLnZpc2libGVNZWRpdW1WaWV3IHtcbiAgICBkaXNwbGF5OiBub25lO1xuICB9XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy5sZ31weCkge1xuICAudG9vbGJhckJ1dHRvblNwYWNlciB7XG4gICAgd2lkdGg6IDE1cHg7XG4gIH1cblxuICAjb3V0ZXJDb250YWluZXIgLmhpZGRlbkxhcmdlVmlldyB7XG4gICAgZGlzcGxheTogbm9uZTtcbiAgfVxuICAjb3V0ZXJDb250YWluZXIgLnZpc2libGVMYXJnZVZpZXcge1xuICAgIGRpc3BsYXk6IGluaGVyaXQ7XG4gIH1cbn1cblxuQG1lZGlhIGFsbCBhbmQgKG1heC13aWR0aDogJHt0aGlzLm1kfXB4KSB7XG4gIC50b29sYmFyQnV0dG9uU3BhY2VyIHtcbiAgICBkaXNwbGF5OiBub25lO1xuICB9XG4gICNvdXRlckNvbnRhaW5lciAuaGlkZGVuTWVkaXVtVmlldyB7XG4gICAgZGlzcGxheTogbm9uZTtcbiAgfVxuICAjb3V0ZXJDb250YWluZXIgLnZpc2libGVNZWRpdW1WaWV3IHtcbiAgICBkaXNwbGF5OiBpbmhlcml0O1xuICB9XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy5zbX1weCkge1xuICAuaGlkZGVuU21hbGxWaWV3LFxuICAuaGlkZGVuU21hbGxWaWV3ICoge1xuICAgIGRpc3BsYXk6IG5vbmU7XG4gIH1cbiAgLnZpc2libGVTbWFsbFZpZXcge1xuICAgIGRpc3BsYXk6IGluaGVyaXQ7XG4gIH1cbiAgLnRvb2xiYXJCdXR0b25TcGFjZXIge1xuICAgIHdpZHRoOiAwO1xuICB9XG4gIGh0bWxbZGlyPSdsdHInXSAuZmluZGJhciB7XG4gICAgbGVmdDogMzhweDtcbiAgfVxuICBodG1sW2Rpcj0ncnRsJ10gLmZpbmRiYXIge1xuICAgIHJpZ2h0OiAzOHB4O1xuICB9XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy54c31weCkge1xuICAjc2NhbGVTZWxlY3RDb250YWluZXIge1xuICAgIGRpc3BsYXk6IG5vbmU7XG4gIH1cbn1cblxuLnZpc2libGVYTFZpZXcsXG4udmlzaWJsZVhYTFZpZXcsXG4udmlzaWJsZVRpbnlWaWV3IHtcbiAgZGlzcGxheTogbm9uZTtcbn1cblxuLmhpZGRlblhMVmlldyxcbi5oaWRkZW5YWExWaWV3IHtcbiAgZGlzcGxheTogdW5zZXQ7XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy54bH1weCkge1xuICAjb3V0ZXJDb250YWluZXIgLmhpZGRlblhMVmlldyB7XG4gICAgZGlzcGxheTogbm9uZTtcbiAgfVxuICAjb3V0ZXJDb250YWluZXIgLnZpc2libGVYTFZpZXcge1xuICAgIGRpc3BsYXk6IGluaGVyaXQ7XG4gIH1cblxuICAjdG9vbGJhclZpZXdlck1pZGRsZSB7XG4gICAgLXdlYmtpdC10cmFuc2Zvcm06IHRyYW5zbGF0ZVgoLTM2JSk7XG4gICAgdHJhbnNmb3JtOiB0cmFuc2xhdGVYKC0zNiUpO1xuICAgIGRpc3BsYXk6IHVuc2V0O1xuICAgIG1hcmdpbjogdW5zZXQ7XG4gICAgbGVmdDogNTAlO1xuICAgIHBvc2l0aW9uOiBhYnNvbHV0ZTtcbiAgfVxufVxuXG5AbWVkaWEgYWxsIGFuZCAobWF4LXdpZHRoOiAke3RoaXMueHhsfXB4KSB7XG4gICNvdXRlckNvbnRhaW5lciAuaGlkZGVuWFhMVmlldyB7XG4gICAgZGlzcGxheTogbm9uZTtcbiAgfVxuICAjb3V0ZXJDb250YWluZXIgLnZpc2libGVYWExWaWV3IHtcbiAgICBkaXNwbGF5OiBpbmhlcml0O1xuICB9XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy5tZH1weCkge1xuICAjdG9vbGJhclZpZXdlck1pZGRsZSB7XG4gICAgLXdlYmtpdC10cmFuc2Zvcm06IHRyYW5zbGF0ZVgoLTI2JSk7XG4gICAgdHJhbnNmb3JtOiB0cmFuc2xhdGVYKC0yNiUpO1xuICB9XG59XG5cbkBtZWRpYSBhbGwgYW5kIChtYXgtd2lkdGg6ICR7dGhpcy54c31weCkge1xuICAuaGlkZGVuVGlueVZpZXcsXG4gIC5oaWRkZW5UaW55VmlldyAqIHtcbiAgICBkaXNwbGF5OiBub25lO1xuICB9XG4gIC52aXNpYmxlVGlueVZpZXcge1xuICAgIGRpc3BsYXk6IGluaGVyaXQ7XG4gIH1cbn1cbiAgYDtcbn1cblxuICBjb25zdHJ1Y3Rvcihwcml2YXRlIHJlbmRlcmVyOiBSZW5kZXJlcjIsIEBJbmplY3QoRE9DVU1FTlQpIHByaXZhdGUgZG9jdW1lbnQ6IGFueSkge31cblxuICBuZ09uSW5pdCgpIHtcbiAgICB0aGlzLmluamVjdFN0eWxlKCk7XG4gIH1cblxuICBuZ09uQ2hhbmdlcygpIHtcbiAgICBjb25zdCBmdWxsV2l0aCA9IHRoaXMuZG9jdW1lbnQuYm9keS5jbGllbnRXaWR0aDtcbiAgICBjb25zdCBwYXJ0aWFsVmlld1NjYWxlID0gZnVsbFdpdGggLyB0aGlzLndpZHRoO1xuICAgIGNvbnN0IHNjYWxlRmFjdG9yID0gcGFydGlhbFZpZXdTY2FsZSAqICh0aGlzLnpvb20gPyB0aGlzLnpvb20gOiAxKTtcblxuICAgIHRoaXMueHMgPSBzY2FsZUZhY3RvciAqIDQ5MDtcbiAgICB0aGlzLnNtID0gc2NhbGVGYWN0b3IgKiA1NjA7XG4gICAgdGhpcy5tZCA9IHNjYWxlRmFjdG9yICogNjEwO1xuICAgIHRoaXMubGcgPSBzY2FsZUZhY3RvciAqIDY2MDtcbiAgICB0aGlzLnhsID0gc2NhbGVGYWN0b3IgKiA3NDA7XG4gICAgdGhpcy54eGwgPSBzY2FsZUZhY3RvciAqIDgzMDtcblxuICAgIGNvbnN0IHN0eWxlcyA9IHRoaXMuZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoJ3BkZi1keW5hbWljLWNzcycpO1xuICAgIGlmIChzdHlsZXMpIHtcbiAgICAgIHN0eWxlcy5pbm5lckhUTUwgPSB0aGlzLnN0eWxlO1xuICAgIH1cbiAgfVxuXG4gIHByaXZhdGUgaW5qZWN0U3R5bGUoKSB7XG4gICAgY29uc3Qgc3R5bGVzID0gdGhpcy5kb2N1bWVudC5jcmVhdGVFbGVtZW50KCdTVFlMRScpIGFzIEhUTUxTdHlsZUVsZW1lbnQ7XG4gICAgc3R5bGVzLmlkID0gJ3BkZi1keW5hbWljLWNzcyc7XG4gICAgc3R5bGVzLmlubmVySFRNTCA9IHRoaXMuc3R5bGU7XG4gICAgdGhpcy5yZW5kZXJlci5hcHBlbmRDaGlsZCh0aGlzLmRvY3VtZW50LmhlYWQsIHN0eWxlcyk7XG4gIH1cblxuICBwdWJsaWMgbmdPbkRlc3Ryb3koKSB7XG4gICAgY29uc3Qgc3R5bGVzID0gdGhpcy5kb2N1bWVudC5nZXRFbGVtZW50QnlJZCgncGRmLWR5bmFtaWMtY3NzJykgYXMgSFRNTEVsZW1lbnQ7XG4gICAgaWYgKHN0eWxlcyAmJiBzdHlsZXMucGFyZW50RWxlbWVudCkge1xuICAgICAgKHN0eWxlcy5wYXJlbnRFbGVtZW50IGFzIGFueSkucmVtb3ZlQ2hpbGQoc3R5bGVzKTtcbiAgICB9XG4gIH1cbn1cbiJdfQ==