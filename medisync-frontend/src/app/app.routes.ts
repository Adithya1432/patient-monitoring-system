import { Routes } from '@angular/router';
import { PatientSignup } from './auth/patient-signup/patient-signup';
import { DoctorSignup } from './auth/doctor-signup/doctor-signup';

export const routes: Routes = [
    { path: 'patient-signup', component: PatientSignup },
    { path: 'doctor-signup', component: DoctorSignup },
    { path: '', redirectTo: 'patient-signup', pathMatch: 'full' }
];
