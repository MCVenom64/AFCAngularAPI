import { Injectable } from '@angular/core';
import {HttpClient, HttpHandler, HttpHeaders} from '@angular/common/http'
import { environment } from '../../environments/environment.development';
import { Players } from './players.model';
import { NgForm } from '@angular/forms';
import { FixtureDetail } from './fixture-detail';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  tokenType:string  = 'Bearer ';
  url:string = environment.apiBaseUrl + 'api/Players'

  //set up for the URL
  externalURL:string = 'https://www.football-data.org/'
  externalToken:string ='51ed0ea1ab92444e85d47d9d8a3ae0eb'
  httpHeader: HttpHeaders =  new HttpHeaders().set('Authorization',this.tokenType + this.externalToken)
  
  list:Players[] = [];
  formData :Players = new Players
  formSubmitted: boolean = false;
  
  constructor(private http : HttpClient) { }

  refreshList(){
    this.http.get(this.url)
    .subscribe({
      next: res=>{
        this.list = res as Players[];
        
      },
      error: err => {console.log(err)}
    }) 
  }

  getTeamsList(){
    this.http.get(this.url,{headers: this.httpHeader})
    .subscribe({
      next: res=>{
        this.list = res as Players[];
        
      },
      error: err => {console.log(err)}
    }) 
  }

  playerUpdate(){
   return this.http.put(this.url + '/'+this.formData.playerId,this.formData)
  }

  playerSubmit(){
    return this.http.post(this.url,this.formData)
   }

   playerDelete(playerId:number){
    return this.http.delete(this.url + '/'+playerId)
   }

  resetForm(form:NgForm){
    form.form.reset()
    this.formData = new Players
    this.formSubmitted = false
  }
}
