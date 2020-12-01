import { Component, OnInit } from '@angular/core';
import { OwnerResponseModel } from '../core/models/owner/owner-response-model';
import { SharedService } from '../core/services/shared.service';

@Component({
  selector: 'app-owner',
  templateUrl: './owner.component.html',
  styleUrls: ['./owner.component.css']
})
export class OwnerComponent implements OnInit {

  edit: boolean = false;
  owner: any;
  constructor(private service: SharedService) { }

  ngOnInit(): void {

  }

  public setOwner(owner: OwnerResponseModel): void {
   this.owner = owner;
}

public setEdit(edit: any): void {
  this.edit = edit;
}

}
