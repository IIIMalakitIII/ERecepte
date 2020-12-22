import { environment } from '../../../environments/environment.prod';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import * as jwt_decode from 'jwt-decode';
import { IRegistrationForm, IUser } from 'src/app/core/interfaces/user.interface';
import { Apollo, ApolloBase, gql } from 'apollo-angular';

const getTokenValue = (): IUser => {
  const tokenValue = JSON.parse(localStorage.getItem('currentUser'));
  if (tokenValue !== null) {
    const model = jwt_decode(tokenValue?.token);
    const userValue = {
      id: model.id,
      rw_role: model.rw_role,
      name: model.name,
      token: tokenValue.token
    };
    return userValue;
  }
  return tokenValue;
};

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<IUser>;
  public currentUser: Observable<IUser>;
  constructor(private apollo: Apollo) {
    this.currentUserSubject = new BehaviorSubject<IUser>(getTokenValue());
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  login(form): Observable<any> {
    /*return this.http.post<any>(environment.apiUrl + `/Account/SignIn`, form.value)
    .pipe(map(user => {
      if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          const userValue = getTokenValue();
          this.currentUserSubject.next(userValue);
      }
      return user;
    }));*/
    return of(null);
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  getToken() {
    return localStorage.getItem('currentUser');
  }

  registration(form: IRegistrationForm): Observable<any> {
    return this.apollo.mutate({
      mutation: gql`
        mutation register($email: String!, $name: String!, $password: String!, $accountType: String!) {
          register(email: $email, name: $name, password: $password, accountType: $accountType) {
            _id
          }
        }
      `,
      variables: {
        ...form
      },
    });
  }
}

