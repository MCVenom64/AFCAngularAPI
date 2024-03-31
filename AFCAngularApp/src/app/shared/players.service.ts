import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { environment } from '../../environments/environment.development';
import { Players } from './players.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {

  url:string = environment.apiBaseUrl + 'api/Players'
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
