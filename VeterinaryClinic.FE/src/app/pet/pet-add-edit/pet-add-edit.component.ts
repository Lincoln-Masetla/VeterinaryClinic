import { DatePipe } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
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

  @Output() response: EventEmitter<any> = new EventEmitter<any>();

  constructor(private service: SharedService, private datepipe: DatePipe) {
    this.setPet();
  }

  ngOnInit(): void {
    this.setPet();
  }

  private setPet(): void {

    if (this.pet) {
      this.petId = this.pet.petId;
      this.ownerId = this.pet.ownerId;
      this.name = this.pet.name;
      this.speciesType = this.pet.speciesType;
      this.birthDate = this.datepipe.transform(this.pet.birthDate, 'yyyy-MM-dd');
      this.colour = this.pet.colour;
      this.notes = this.pet.notes;
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
        const res = {
          message: 'Successfully Created Pet',
          error: false,
          success: true
        };
        this.response.emit(res);
      },
      () => {
        const res = {
          message: 'Oops!! Something went wrong while Creating Pet :( try again later',
          error: true,
          success: false,
        };

        this.response.emit(res);
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

    this.service.UpdatePet(addRequest).subscribe(
      (data) => {

        const res = {
          message: 'Successfully Updated Pet',
          error: false,
          success: true
        };
        this.response.emit(res);
      },
      () => {
        const res = {
          message: 'Oops!! Something went wrong while Updating Pet :( try again later',
          error: true,
          success: false
        };
        this.response.emit(res);
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
