import { describe, it, expect, beforeEach, vi } from 'vitest';
import { TodoComponent } from './todo.component';

class MockTodoService {
  todos = () => [] as any[];
  loadTodos = vi.fn();
  addTodo = vi.fn();
  deleteTodo = vi.fn();
}

describe('TodoComponent (unit)', () => {
  let component: TodoComponent;
  let mockService: MockTodoService;

  beforeEach(() => {
    mockService = new MockTodoService();
    // create instance of component without Angular DI
    component = Object.create(TodoComponent.prototype) as TodoComponent;
    component.todoService = (mockService as unknown) as any;
    component.newTodoTitle = '';
  });

  it('should call loadTodos on init', () => {
    component.ngOnInit();
    expect(mockService.loadTodos).toHaveBeenCalled();
  });

  it('should call addTodo and clear input when submitting', () => {
    component.newTodoTitle = 'Test Task';
    component.submit();
    expect(mockService.addTodo).toHaveBeenCalledWith('Test Task');
    expect(component.newTodoTitle).toBe('');
  });

  it('should use todoService.todos when rendering (class-level)', () => {    
    (mockService as any).todos = () => [{ id: '1', title: 'One' }];
    expect(component.todoService.todos()[0].title).toBe('One');
  });
});
