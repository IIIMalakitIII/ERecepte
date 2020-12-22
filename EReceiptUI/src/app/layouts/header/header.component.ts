import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Role } from 'src/app/core/extension/role.enum';
import { IUser } from 'src/app/core/interfaces/user.interface';
import { AuthenticationService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  currentUser: IUser;
  roles = Role;
  private currentUserSubject: BehaviorSubject<string>;
  public currentSubject: Observable<string>;
  constructor(private authService: AuthenticationService, private router: Router, private translate: TranslateService) {
    this.currentUserSubject = new BehaviorSubject<string>(localStorage.getItem('localization'));
    this.currentSubject = this.currentUserSubject.asObservable();
    authService.currentUser.subscribe( x => this.currentUser = x);
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  ngOnInit(): void {
    if (!this.currentUserValue) {
      this.localizationEN();
    }
    this.translate.use(this.currentUserValue);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  localizationUA() {
    localStorage.setItem('localization', 'ua');
    this.currentUserSubject.next('ua');
    this.translate.use(this.currentUserValue);
  }

  localizationEN() {
    localStorage.setItem('localization', 'en');
    this.currentUserSubject.next('en');
    this.translate.use(this.currentUserValue);
  }

}
