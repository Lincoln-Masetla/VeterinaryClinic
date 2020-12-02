import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PetResponseModel } from 'src/app/core/models/pet/pet-response-model';
import { SharedService } from 'src/app/core/services/shared.service';

@Component({
  selector: 'app-pet-show',
  templateUrl: './pet-show.component.html',
  styleUrls: ['./pet-show.component.css']
})
export class PetShowComponent implements OnInit, OnDestroy {
  private readonly onDestroy = new Subject<void>();
  pets: PetResponseModel[] = [];
  modalTitle: string | undefined;
  @Input() ownerId: any;
  pet: any;
  edit: boolean = false;
  @Output() response: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('closebutton') closebutton: any;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.getPets();
  }

  ngOnDestroy(): void {
    this.onDestroy.next();
  }

  getPets(): void {
    this.service
      .getPetByOwner(this.ownerId)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((data) => {
        this.pets = data;
      });
  }

  addPet(): void {
    this.pet = null;
    this.edit = true;
  }

  editPet(item: PetResponseModel): void {
    this.pet = item;
    this.edit = true;
  }

  deletePet(pet: PetResponseModel): void {
    this.service.DeletePet( pet.petId, pet.ownerId).subscribe( data => this.getPets());
  }

  closePet(): void {
    this.edit = false;
    this.getPets();
    this.closebutton.nativeElement.click();
  }

  public setResponse(res: any): void {
    this.closebutton.nativeElement.click();
    this.response.emit(res);
    this.getPets();
    this.edit = false;
  }

}
