import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OwnerResponseModel } from '../models/owner/owner-response-model';
import { OwnerRequestModel } from '../models/owner/owner-request-model';
import { PetRequestModel } from '../models/pet/pet-request-model';
import { PetResponseModel } from '../models/pet/pet-response-model';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl = 'https://localhost:44345/api';
  constructor(private http: HttpClient) { }

  getOwners(): Observable<OwnerResponseModel[]>{
    return this.http.get<OwnerResponseModel[]>(`${this.APIUrl}/owners`);
  }

  createOwner(request: OwnerRequestModel ): Observable<OwnerResponseModel>{
    return this.http.post<OwnerResponseModel>(`${this.APIUrl}/owners`, request);
  }

  updateOwner(request: OwnerResponseModel ): Observable<OwnerResponseModel>{
    return this.http.put<OwnerResponseModel>(`${this.APIUrl}/owners`, request);
  }

  getPetByOwner(ownerId: number): Observable<PetResponseModel[]>{
    return this.http.get<PetResponseModel[]>(`${this.APIUrl}/pets/${ownerId}`);
  }

  getPet(petId: number, ownerId: number): Observable<PetResponseModel[]>{
    return this.http.get<PetResponseModel[]>(`${this.APIUrl}/pets/${petId}/${ownerId}`);
  }

  createPet(request: PetRequestModel ): Observable<PetResponseModel>{
    return this.http.post<PetResponseModel>(`${this.APIUrl}/pets`, request);
  }

  UpdatePet(request: PetResponseModel ): Observable<PetResponseModel>{
    return this.http.post<PetResponseModel>(`${this.APIUrl}/pets`, request);
  }

  DeletePet(petId: number, ownerId: number): Observable<PetResponseModel>{
    return this.http.delete<PetResponseModel>(`${this.APIUrl}/pets/${petId}/${ownerId}`);
  }
}
