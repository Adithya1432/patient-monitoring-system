import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';

/* Angular Material */
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

import { Auth } from '../../core/services/auth';

@Component({
  selector: 'app-doctor-signup',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatSelectModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './doctor-signup.html',
  styleUrl: './doctor-signup.css',
})
export class DoctorSignup {
  signupForm!: FormGroup;

  specialties: string[] = [
    'General Physician', 'Cardiologist', 'Dermatologist',
    'Neurologist', 'Pediatrician', 'Orthopedist', 'Psychiatrist'
  ];

  consultationTypes: string[] = ['Online', 'In-Person'];

  constructor(private fb: FormBuilder, private authService: Auth) {
    this.signupForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      registrationNumber: ['', Validators.required],
      specialty: ['', Validators.required],
      yearsOfExperience: ['', [Validators.required, Validators.min(0)]],
      consultationType: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required]
    });
  }

  submit() {
    if (this.signupForm.valid) {
      const formValue = this.signupForm.value;

      // Format DOB to YYYY-MM-DD (Local Time)
      if (formValue.dateOfBirth instanceof Date) {
        const dobDate = formValue.dateOfBirth;
        const year = dobDate.getFullYear();
        const month = ('0' + (dobDate.getMonth() + 1)).slice(-2);
        const day = ('0' + dobDate.getDate()).slice(-2);
        formValue.dateOfBirth = `${year}-${month}-${day}`;
      }

      console.log('Submitting Doctor:', formValue);
      this.authService.doctorSignup(formValue).subscribe({
        next: (response) => {
          console.log('Doctor Signup successful', response);
          // Handle success
        },
        error: (error) => {
          console.error('Doctor Signup failed', error);
          // Handle error
        }
      });
    }
  }
}
