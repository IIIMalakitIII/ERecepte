import { AuthenticationService } from './../core/services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private authenticationService: AuthenticationService, private router: Router,) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const userRoles = this.authenticationService.currentUserValue.er_role;
    const allowedRoles = next.data.roles;
    const matchingRoles = allowedRoles.indexOf(userRoles);

    if (matchingRoles === -1 && next.data.tryToRedirect) {
      this.router.navigate([next.data.tryToRedirect]);
    }
    return matchingRoles !== -1;
  }
}
