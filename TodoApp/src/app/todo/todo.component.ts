import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TodoService } from './todo.service';

@Component({
  selector: 'app-todo',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './todo.html',
  styleUrls: ['./todo.css']
})
export class TodoComponent implements OnInit {
  todoService = inject(TodoService);
  newTodoTitle = '';

  ngOnInit() {
    this.todoService.loadTodos();
  }

  submit() {
    if (!this.newTodoTitle.trim()) return;
    this.todoService.addTodo(this.newTodoTitle.trim());
    this.newTodoTitle = '';
  }
}
