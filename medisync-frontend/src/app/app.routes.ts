import { Routes } from '@angular/router';
import { PatientSignup } from './auth/patient-signup/patient-signup';
import { DoctorSignup } from './auth/doctor-signup/doctor-signup';
import { LoginComponent } from './auth/login/login';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'patient-signup', component: PatientSignup },
    { path: 'doctor-signup', component: DoctorSignup },

    {
        path: 'doctor',
        loadComponent: () => import('./core/layouts/doctor-layout/doctor-layout').then(m => m.DoctorLayout),
        children: [
            {
                path: 'dashboard',
                loadComponent: () => import('./doctor/dashboard/doctor-dashboard/doctor-dashboard').then(m => m.DoctorDashboard)
            },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
        ]
    },
    {
        path: 'patient',
        loadComponent: () => import('./core/layouts/patient-layout/patient-layout').then(m => m.PatientLayout),
        children: [
            {
                path: 'dashboard',
                loadComponent: () => import('./patient/dashboard/patient-dashboard/patient-dashboard').then(m => m.PatientDashboard)
            },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
        ]
    },
    {
        path: 'admin',
        loadComponent: () => import('./core/layouts/admin-layout/admin-layout').then(m => m.AdminLayout),
        children: [
            {
                path: 'dashboard',
                loadComponent: () => import('./admin/dashboard/admin-dashboard/admin-dashboard').then(m => m.AdminDashboard)
            },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
        ]
    },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
