import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { IUser } from './core/interfaces/user.interface';
import { AuthenticationService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'EReceiptUI';
  currentUser: IUser;

  private currentUserSubject: BehaviorSubject<string>;
  public currentSubject: Observable<string>;
  constructor(private authService: AuthenticationService, private translate: TranslateService, private router: Router) {
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
