import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OwnerComponent } from './owner/owner.component';
import { ShowComponent } from './owner/show/show.component';
import { AddEditComponent } from './owner/add-edit/add-edit.component';
import { PetComponent } from './pet/pet.component';
import { SharedService } from './core/services/shared.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PetShowComponent } from './pet/pet-show/pet-show.component';
import { PetAddEditComponent } from './pet/pet-add-edit/pet-add-edit.component';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    OwnerComponent,
    ShowComponent,
    AddEditComponent,
    PetComponent,
    PetShowComponent,
    PetAddEditComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [SharedService, DatePipe],
  bootstrap: [AppComponent],
})
export class AppModule {}
