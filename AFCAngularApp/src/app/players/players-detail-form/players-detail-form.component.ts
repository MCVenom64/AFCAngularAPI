import { Component } from '@angular/core';
import { PlayersComponent } from "../players.component";
import { PlayersService } from '../../shared/players.service';
import {  FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Players } from '../../shared/players.model';
import { ToastrService } from 'ngx-toastr';
@Component({
    selector: 'app-players-detail-form',
    standalone: true,
    templateUrl: './players-detail-form.component.html',
    styles: ``,
    imports: [PlayersComponent,CommonModule,FormsModule]
})
export class PlayersDetailFormComponent {
   

    constructor(public service : PlayersService,private toastr:ToastrService){

    }

    onSubmit(form:NgForm){
        this.service.formSubmitted = true
        if(form.valid){
if(this.service.formData.playerId == 0){
 this.addPlayer(form)
}
else{
    this.updatePlayer(form)
}
      
}
    
    }
    addPlayer(form:NgForm){
        this.service.playerSubmit()
        .subscribe({
            next: res=>{
            this.service.list = res as Players[]
            this.service.resetForm(form);
            this.toastr.success('Saved sucessfully', 'Player Detail')
           },
            error:err => {console.log(err)}
        })
    }
    updatePlayer(form:NgForm){

       this.service.playerUpdate()
       .subscribe({
        next: res=>{
        this.service.list = res as Players[]
        this.service.resetForm(form);
        this.toastr.info('Updated sucessfully', 'Player Detail')
       },
        error:err => {console.log(err)}
    })

       
    }
}
