import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TodoService } from './todo.service';
import { environment } from '../../environments/environment';
import { TodoItem } from './todo.model';
import { describe, it, expect, beforeEach, afterEach } from 'vitest';

describe('TodoService', () => {
  let service: TodoService;
  let httpMock: HttpTestingController;
  const api = `${environment.apiBaseUrl}/todos`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TodoService]
    });

    service = TestBed.inject(TodoService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should load todos and update signal', () => {
    const mock: TodoItem[] = [{ id: '1', title: 'A', completed: false } as any];
    service.loadTodos();
    const req = httpMock.expectOne(api);
    expect(req.request.method).toBe('GET');
    req.flush(mock);
    expect(service.todos()).toEqual(mock);
  });

  it('should add todo and prepend to list', () => {
    const initial: TodoItem[] = [];
    // initialize via loadTodos
    service.loadTodos();
    httpMock.expectOne(api).flush(initial);

    const newTodo: TodoItem = { id: '2', title: 'New', completed: false } as any;
    service.addTodo('New');
    const req = httpMock.expectOne(api);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ title: 'New' });
    req.flush(newTodo);

    expect(service.todos()[0]).toEqual(newTodo);
  });

  it('should delete todo and remove from list', () => {
    const items: TodoItem[] = [{ id: '3', title: 'T', completed: false } as any];
    service.loadTodos();
    httpMock.expectOne(api).flush(items);

    service.deleteTodo('3');
    const req = httpMock.expectOne(`${api}/3`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});

    expect(service.todos().length).toBe(0);
  });
});
