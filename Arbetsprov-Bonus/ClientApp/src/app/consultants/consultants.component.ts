import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-consultants',
  templateUrl: './consultants.component.html'
})
export class ConsultantsComponent implements OnInit {
  public consultants: Consultant[] = [];
  public selectedConsultant?: Consultant;
  public editingConsultant?: Consultant;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.loadConsultants();
  }

  loadConsultants() {
    this.http.get<Consultant[]>(this.baseUrl + 'consultant').subscribe(
      result => {
        this.consultants = result;
      },
      error => console.error(error)
    );
  }

  viewConsultant(consultant: Consultant) {
    this.selectedConsultant = consultant;
  }

  editConsultant(consultant: Consultant) {
    this.editingConsultant = { ...consultant };
  }

  saveEdit(consultant: Consultant) {
    this.http.put(this.baseUrl + 'consultant/' + consultant.id, consultant).subscribe(
      () => {
        this.loadConsultants(); // Reload the data
        this.editingConsultant = undefined;
      },
      error => console.error(error)
    );
  }

  deleteConsultant(id: number) {
    this.http.delete(this.baseUrl + 'consultant/' + id).subscribe(
      () => {
        this.loadConsultants(); // Reload the data
      },
      error => console.error(error)
    );
  }
}

interface Consultant {
  id: number;
  firstName: string;
  lastName: string;
  startDate: Date;
  totalHoursWorked: number;
}
