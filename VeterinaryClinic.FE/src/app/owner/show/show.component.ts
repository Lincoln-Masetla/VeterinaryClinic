import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { OwnerRequestModel } from 'src/app/core/models/owner/owner-request-model';
import { OwnerResponseModel } from 'src/app/core/models/owner/owner-response-model';
import { SharedService } from 'src/app/core/services/shared.service';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css'],
})
export class ShowComponent implements OnInit, OnDestroy {
  private readonly onDestroy = new Subject<void>();
  owners: OwnerResponseModel[] = [];
  modalTitle: string | undefined;

 @Output() owner: EventEmitter<any> = new EventEmitter<any>();

  @Output() edit: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.getOwners();
  }

  ngOnDestroy(): void {
    this.onDestroy.next();
  }

  getOwners(): void {
    this.service
      .getOwners()
      .pipe(takeUntil(this.onDestroy))
      .subscribe((data) => {
        this.owners = data;
      });
  }

  addOwner(): void {

    this.owner.emit(null);
    this.edit.emit(true);
  }

  editOwner(item: OwnerResponseModel): void {

    this.owner.emit(item);
    this.edit.emit(true);
  }

  closeOwner(): void {
    this.edit.emit(false);
    this.getOwners();
  }
}
