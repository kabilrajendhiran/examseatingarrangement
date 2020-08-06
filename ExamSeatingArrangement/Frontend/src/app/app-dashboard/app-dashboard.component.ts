import { Component, OnInit } from '@angular/core';
import {userParams} from '../../shared/models/userParams';
import {AuthService} from '../../services/auth.service';
import {NavbarService} from '../../services/navbar.service';

@Component({
  selector: 'app-app-dashboard',
  templateUrl: './app-dashboard.component.html',
  styleUrls: ['./app-dashboard.component.css']
})
export class AppDashboardComponent implements OnInit {

  userData:userParams;

  constructor(private auth:AuthService,public nav:NavbarService) { }

  ngOnInit(): void {
    this.auth.currentUserData.subscribe(userparams=>this.userData=userparams);
    this.nav.show();

  }



}
