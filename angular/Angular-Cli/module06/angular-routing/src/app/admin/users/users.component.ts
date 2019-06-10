import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users:any[];
  hasPermission:boolean = true;

  constructor() { }

  ngOnInit() {
    if(this.hasPermission){
      this.getUsers()
        .then(users => this.users = users)
        .catch(e => console.log(e.message));
    }
    else{
      this.users = [];
    }
  }

  async getUsers(){
    return [
      {name:'john', email:'sdfdsf@sdfs.com'},
      {name:'peter', email:'sfsdfdfdsfs@sdfs.com'}
    ];
  }

}
