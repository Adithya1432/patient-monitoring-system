import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-book-appointment',
  imports: [CommonModule, FormsModule],
  templateUrl: './book-appointment.html',
  styleUrl: './book-appointment.css',
})
export class BookAppointment {
  
  speciality = '';
  preferredDate = '';
  preferredTime = '';
  message = '';
  loading = false;

  constructor(private http: HttpClient) {}

  bookAppointment() {
    this.loading = true;
    this.message = '';

    const payload = {
      patientId: '7c9e6679-7425-40de-944b-e07fc1f90ae7', // later from auth
      speciality: this.speciality,
      preferredDate: this.preferredDate,      // YYYY-MM-DD
      preferredTime: this.preferredTime + ':00' // HH:mm:ss
    };

    this.http.post<any>(
      'https://localhost:7100/api/appointments/book',
      payload
    ).subscribe({
      next: (res) => {
        this.message =
          `Appointment booked successfully.
           Doctor: ${res.doctorId}
           Time: ${res.scheduledStartTime}`;
        this.loading = false;
      },
      error: (err) => {
        this.message =
          err.error?.message || 'No available slot';
        this.loading = false;
      }
    });
  }
}
