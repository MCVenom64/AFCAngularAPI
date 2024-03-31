import { Component,OnInit } from '@angular/core';
import { PlayersDetailFormComponent } from "./players-detail-form/players-detail-form.component";
import { PlayersService } from '../shared/players.service';
import { Players } from '../shared/players.model';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-players',
    standalone: true,
    templateUrl: './players.component.html',
    styles: ``,
    imports: [PlayersDetailFormComponent]
})
export class PlayersComponent implements OnInit {

  constructor(public service : PlayersService, private toastr: ToastrService){

  }
  ngOnInit(): void {
 this.service.refreshList();
  }
  populateForm(selectedRecord:Players){
    this.service.formData = Object.assign({},selectedRecord);
  }

  deletePlayer(id:number){
if(confirm('Are you sure you want to delete this record?')){
    this.service.playerDelete(id)
    .subscribe({
     next: res=>{
     this.service.list = res as Players[]
     this.toastr.error('Delete sucessfully', 'Player Detail')
    },
     error:err => {console.log(err)}
 })
}
}}
