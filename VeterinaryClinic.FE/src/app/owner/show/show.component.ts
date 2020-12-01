import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { OwnerResponseModel } from 'src/app/core/models/owner/owner-response-model';
import { SharedService } from 'src/app/core/services/shared.service';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css'],
})
export class ShowComponent implements  OnDestroy {

  private readonly onDestroy = new Subject<void>();
  owners: OwnerResponseModel[] = [];

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.getOwners();
  }

  ngOnDestroy(): void{
    this.onDestroy.next();
  }

  getOwners(): void {
    this.service.getOwners()
    .pipe(takeUntil(this.onDestroy))
    .subscribe((data) =>
    {
      this.owners = data;
    });

  }
}
