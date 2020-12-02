import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectMultipleControlValueAccessor } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OwnerRequestModel } from 'src/app/core/models/owner/owner-request-model';
import { OwnerResponseModel } from 'src/app/core/models/owner/owner-response-model';
import { SharedService } from 'src/app/core/services/shared.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css'],
})
export class AddEditComponent implements OnInit {
  @Input() owner: any;
  ownerId: any;
  ownerName: any;
  ownerCellNo: any;
  ownerAddress: any;
  ownerEmail: any;
  error: boolean = false;
  success: boolean = false;
  message: any;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.setOwner();
  }

  private setOwner(): void {
    if (this.owner) {
      this.ownerId = this.owner.ownerId;
      this.ownerName = this.owner.name;
      this.ownerEmail = this.owner.email;
      this.ownerCellNo = this.owner.cellNo;
      this.ownerAddress = this.owner.address;
    }
  }

  addOwner(): void {
    const addRequest: OwnerRequestModel = {
      name: this.ownerName,
      email: this.ownerEmail,
      cellNo: this.ownerCellNo,
      address: this.ownerAddress,
    };

    this.service.createOwner(addRequest).subscribe(
      (data) => {
        this.owner = data;
        this.ownerId = this.owner.ownerId;
        this.success = true;
        this.message = 'Successfully Created Owner';
      },
      () => {
        this.message =
          'Oops!! Something went wrong while Creating Owner :( try again later';
        this.error = true;
      }
    );
  }

  updateOwner(): void {
    const updateRequest: OwnerResponseModel = {
      ownerId: this.ownerId ,
      name: this.ownerName,
      email: this.ownerEmail,
      cellNo: this.ownerCellNo,
      address: this.ownerAddress,
    };

    this.service.updateOwner(updateRequest).subscribe(
      (data) => {
        this.success = true;
        this.message = 'Successfully Updated Owner';
      },
      () => {
        this.message =
          'Oops!! Something went wrong while Updating Owner :( try again later';
        this.error = true;
      }
    );
  }

  closeSuccessAlert(): void {
    this.success = false;
  }

  closeErrorAlert(): void {
    this.error = false;
  }

  submit(): void {
    if (this.owner) {
      this.updateOwner();
    } else {
      this.addOwner();
    }
  }

  public setResponse(res: any): void {
    this.error = res.error;
    this.success = res.success;
    this.message = res.message;
  }

  cancel(): void {
    window.location.reload();
  }
}
