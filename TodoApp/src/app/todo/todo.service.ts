import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { TodoItem } from './todo.model';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiBaseUrl}/todos`;

  // Read-only signal state prevents components from modifying array directly
  #todos = signal<TodoItem[]>([]);
  todos = this.#todos.asReadonly();

  loadTodos() {
    this.http.get<TodoItem[]>(this.apiUrl).subscribe(data => this.#todos.set(data));
  }

  addTodo(title: string) {
    this.http.post<TodoItem>(this.apiUrl, { title }).subscribe(newTodo => {
      this.#todos.update(current => [newTodo, ...current]);
    });
  }

  deleteTodo(id: string) {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
      this.#todos.update(current => current.filter(t => t.id !== id));
    });
  }
}
