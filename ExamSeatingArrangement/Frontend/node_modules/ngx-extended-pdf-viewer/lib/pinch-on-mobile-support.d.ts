import { NgZone } from '@angular/core';
export declare class PinchOnMobileSupport {
    private _zone;
    private viewer;
    private container;
    private startX;
    private startY;
    private initialPinchDistance;
    private pinchScale;
    constructor(_zone: NgZone);
    private isMobile;
    private onViewerTouchStart;
    private onViewerTouchMove;
    private onViewerTouchEnd;
    private resetPinchZoomParams;
    initializePinchZoom(): void;
    destroyPinchZoom(): void;
}
