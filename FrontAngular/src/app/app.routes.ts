import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard',
    loadComponent: () => import('./components/dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [AuthGuard]
  },
  //   // Otras rutas protegidas
  //   { 
  //     path: 'profile', 
  //     loadComponent: () => import('./profile/profile.component').then(m => m.ProfileComponent),
  //     canActivate: [AuthGuard]
  //   },
  //   // Ruta por defecto - redirige al dashboard si está autenticado, o al login si no lo está
  //   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  //   // Ruta para cualquier otra URL no definida
  { path: '**', redirectTo: '/login' }
];
