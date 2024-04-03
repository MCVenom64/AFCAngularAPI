import { Component } from '@angular/core';
import { PlayersComponent } from "../players.component";
import { PlayersService } from '../../shared/players.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Players } from '../../shared/players.model';
import { ToastrService } from 'ngx-toastr';
import { from } from "linq-to-typescript";
@Component({
    selector: 'app-players-detail-form',
    standalone: true,
    templateUrl: './players-detail-form.component.html',
    styles: ``,
    imports: [PlayersComponent, CommonModule, FormsModule]
})
export class PlayersDetailFormComponent {
    //List of players - to extend thisthi will be an aditional call to the API to get a list of players from database.
    positions = ['GoalKeeper', 'Defender', 'Midfielder', 'Striker'];

    constructor(public service: PlayersService, private toastr: ToastrService) {    
    }

    onSubmit(form: NgForm) {
        this.service.formSubmitted = true
        if (form.valid) {
            if (this.service.formData.playerId == 0) {
                this.addPlayer(form)
            }
            else {
                this.updatePlayer(form)
            }

        }

    }
    addPlayer(form: NgForm) {
        this.service.playerSubmit()
            .subscribe({
                next: res => {
                    this.service.list = res as Players[]
                    this.service.resetForm(form);
                    this.toastr.success('Saved sucessfully', 'Player Detail')
                },
                error: err => { console.log(err) }
            })
    }
    updatePlayer(form: NgForm) {

        this.service.playerUpdate()
            .subscribe({
                next: res => {
                    this.service.list = res as Players[]
                    this.service.resetForm(form);
                    this.toastr.info('Updated sucessfully', 'Player Detail')
                },
                error: err => { console.log(err) }
            })


    }
    // jerseyNumberValidation(form: NgForm){
    //     return (from(this.service.list).any((x)=> x.jerseyNumber == this.service.formData.jerseyNumber && x.playerId != this.service.formData.playerId));
    // }
}
