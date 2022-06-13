import { Component } from '@angular/core';
import { Interest } from './../../_models/Interest';
import { AdminService } from './../../_services/admin.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-interest-management',
  templateUrl: './interest-management.component.html',
  styleUrls: ['./interest-management.component.css']
})
export class InterestManagementComponent {

  todos: any;
  todoObj: any;
  newTodo: string;
  interests: Interest[] = [];

  constructor(
    private adminService: AdminService,
    private alertify: AlertifyService) {

    this.adminService.getInterest()
      .subscribe(interests => this.interests = interests);

    this.newTodo = '';
    this.todos = [];
  }

  addTodo(event) {
    this.todoObj = {
      name: this.newTodo
    }
    this.todos.push(this.todoObj);
    this.newTodo = '';
    event.preventDefault();
  }

  addToInterest(interest, index) {
    this.interests.push(interest);
    this.adminService.createInterest(interest)
      .subscribe(() => {
        this.alertify.success('successfully added new interest');
      },
      () => {
        this.alertify.error("Somegthing went wrong");
        this.interests.splice(this.interests.indexOf(interest), 1);
      });
    
    this.deleteTodo(index)
  }

  deleteTodo(index) {
    this.todos.splice(index, 1);
  }

  deleteSelectedTodos() {
    //need ES5 to reverse loop in order to splice by index
    for(var i=(this.todos.length -1); i > -1; i--) {
      if(this.todos[i].completed) {
        this.todos.splice(i, 1);
      }
    }
  }
}
