import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private baseUrl = 'https://localhost:7000/api/User';

  constructor(private http: HttpClient) { }

  patientSignup(data: any) {
    return this.http.post(`${this.baseUrl}/patinetSignup`, data);
  }

  doctorSignup(data: any) {
    return this.http.post(`${this.baseUrl}/doctorSignup`, data);
  }
}
