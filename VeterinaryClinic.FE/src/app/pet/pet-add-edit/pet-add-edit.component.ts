import { Component, Input, OnInit } from '@angular/core';
import { PetRequestModel } from 'src/app/core/models/pet/pet-request-model';
import { PetResponseModel } from 'src/app/core/models/pet/pet-response-model';
import { SharedService } from 'src/app/core/services/shared.service';

@Component({
  selector: 'app-pet-add-edit',
  templateUrl: './pet-add-edit.component.html',
  styleUrls: ['./pet-add-edit.component.css'],
})
export class PetAddEditComponent implements OnInit {
  @Input() pet: any;
  @Input() ownerId: any;
  petId: any;
  name: any;
  speciesType: any;
  birthDate: any;
  colour: any;
  notes: any;
  error: boolean = false;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.setPet();
  }

  private setPet(): void {
    if (this.pet) {
      this.petId = this.pet.petId;
      this.ownerId = this.pet.ownerId;
      this.name = this.pet.name;
      this.speciesType = this.pet.speciesType;
      this.birthDate = this.pet.birthDate;
      this.colour = this.pet.colour;
      this.notes = this.pet.note;
    }
  }

  addPet(): void {
    const addRequest: PetRequestModel = {
      ownerId : this.ownerId,
      name: this.name,
      speciesType : this.speciesType,
      birthDate : this.birthDate,
      colour : this.colour,
      notes : this.notes
    };

    this.service.createPet(addRequest).subscribe(
      (data) => {
        window.location.reload();
      },
      () => {
        this.error = true;
      }
    );
  }

  updatePet(): void {
    const addRequest: PetResponseModel = {
      ownerId : this.ownerId,
      name: this.name,
      speciesType : this.speciesType,
      birthDate : this.birthDate,
      colour : this.colour,
      notes : this.notes,
      petId: this.petId
    };

    this.service.createPet(addRequest).subscribe(
      (data) => {
        window.location.reload();
      },
      () => {
        this.error = true;
      }
    );
  }

  submit(): void {
    if (this.pet) {
      this.updatePet();
    } else {
      this.addPet();
    }
  }
}
