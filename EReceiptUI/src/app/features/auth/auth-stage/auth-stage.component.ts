import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTabGroup } from '@angular/material/tabs';
import { SignInComponent } from '../sign-in/sign-in.component';
import { SignUpComponent } from '../sign-up/sign-up.component';

@Component({
  selector: 'app-auth-stage',
  templateUrl: './auth-stage.component.html',
  styleUrls: ['./auth-stage.component.scss']
})
export class AuthStageComponent implements OnInit {

  @ViewChild(SignInComponent) signIn: SignInComponent;
  @ViewChild(SignUpComponent) signUp: SignUpComponent;
  @ViewChild('tabs') tabGroup: MatTabGroup;

  constructor() { }

  ngOnInit(): void {
  }

  onTabChanged(): void {
    this.signIn.clearForm();
    this.signUp.clearForm();
  }

  openSignIn(event: any): void {
    this.tabGroup.selectedIndex = 2;
  }
}
