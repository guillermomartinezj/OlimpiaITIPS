import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Router } from '@angular/router';

export interface AuthResponse {
  token: string;
  // Añade otros campos según la respuesta de tu API
}

export interface User {
  username: string;
  // Añade otros campos según necesites
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());
  public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.checkToken();
  }

  login(username: string, password: string): Observable<AuthResponse> {
    // Reemplaza esta URL con la de tu API
    return this.http.post<AuthResponse>('https://localhost:7156/api/Login/Authenticate', { username, password })
      .pipe(
        tap(response => {
          this.setSession(response);
          // Aquí podrías decodificar el JWT para obtener información del usuario
          this.currentUserSubject.next({ username });
          this.isAuthenticatedSubject.next(true);
        }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('Error en la solicitud HTTP:', error);
    return throwError(() => error);
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    this.currentUserSubject.next(null);
    this.isAuthenticatedSubject.next(false);
    this.router.navigate(['/login']);
  }

  private setSession(authResult: AuthResponse): void {
    localStorage.setItem(this.TOKEN_KEY, authResult.token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  private hasToken(): boolean {
    return !!this.getToken();
  }

  private checkToken(): void {
    const token = this.getToken();
    if (token) {
      // Aquí podrías verificar si el token es válido (no expirado)
      // Por simplicidad, asumimos que si hay token, el usuario está autenticado
      this.isAuthenticatedSubject.next(true);
      // También podrías decodificar el token para obtener info del usuario
      this.currentUserSubject.next({ username: 'usuario' });
    }
  }

  isAuthenticated(): boolean {
    return this.isAuthenticatedSubject.value;
  }
}
