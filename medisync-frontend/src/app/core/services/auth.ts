import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DoctorSignupRequest } from '../../shared/models/requests/doctor-signup.request';
import { PatientSignupRequest } from '../../shared/models/requests/patient-signup.request';
import { LoginRequest } from '../../shared/models/requests/login.request';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private baseUrl = 'https://localhost:7000/api';

  constructor(private http: HttpClient) { }

  patientSignup(data: PatientSignupRequest) {
    return this.http.post(`${this.baseUrl}/User/patinetSignup`, data);
  }

  doctorSignup(data: DoctorSignupRequest) {
    return this.http.post(`${this.baseUrl}/User/doctorSignup`, data);
  }

  login(data: LoginRequest) {
    return this.http.post(`${this.baseUrl}/Auth/login`, data);
  }
}
