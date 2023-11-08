import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
interface Consultant {
  id: number;
  firstName: string;
  lastName: string;
  startDate: Date;
  debitedHours: number;
  bonus?: number;
}

@Component({
  selector: 'app-bonus',
  templateUrl: './bonus.component.html'
})
export class BonusComponent implements OnInit {
  public consultants: Consultant[] = [];
  public totalNetResult: number = 0;
  private bonusPool: number = 0;
  private totalLoyaltyPoints: number = 0;
  public totalBonus: number = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.loadConsultants();
  }

  loadConsultants() {
    this.http.get<Consultant[]>(this.baseUrl + 'consultant').subscribe(
      result => {
        this.consultants = result;
        // Initialize debited hours for each consultant
        this.consultants.forEach(consultant => consultant.debitedHours = 0);
      },
      error => console.error(error)
    );
  }

  allFieldsFilled(): boolean {
    // Check if netResult is filled and all consultants have debitedHours entered
    return this.totalNetResult > 0 && this.consultants.every(c => c.debitedHours > 0);
  }

  onNetResultChange(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value;

    if (value) {
      this.totalNetResult = parseFloat(value);
      this.calculateBonusPool();
    } else {
      // Handle the case where the input is empty or invalid
      this.totalNetResult = 0;
      this.calculateBonusPool();
    }
  }

  calculateBonusPool() {
    // Assuming 5% of the total net result goes to the bonus pool
    this.bonusPool = this.totalNetResult * 0.05;
    this.calculateBonuses();
  }

  calculateBonuses() {

    if (!this.allFieldsFilled()) {
      this.consultants.forEach(consultant => consultant.bonus = undefined);
      this.totalBonus = 0; // Reset total bonus when not all fields are filled
      return;
    }

    // Calculate the total loyalty points
    this.totalLoyaltyPoints = this.consultants.reduce((sum, consultant) => sum + this.calculateLoyaltyFactor(consultant) * consultant.debitedHours, 0);

    // Calculate each consultant's bonus
    let total = 0;
    this.consultants.forEach(consultant => {
      const loyaltyFactor = this.calculateLoyaltyFactor(consultant);
      consultant.bonus = (loyaltyFactor * consultant.debitedHours / this.totalLoyaltyPoints) * this.bonusPool;
      total += consultant.bonus; // Sum up the bonuses for the total
    });
    this.totalBonus = total; // Update the total bonus
  }

  calculateLoyaltyFactor(consultant: Consultant): number {
    const employmentDurationInYears = new Date().getFullYear() - new Date(consultant.startDate).getFullYear();
    if (employmentDurationInYears >= 5) return 1.5;
    if (employmentDurationInYears < 1) return 1;
    return 1 + (0.1 * employmentDurationInYears);
  }

  calculateTotalBonus(): number {
    return this.consultants.reduce((acc, c) => acc + (c.bonus || 0), 0);
  }

}

