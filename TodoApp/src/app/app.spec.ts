import { describe, it, expect } from 'vitest';
import { App } from './app';

describe('App (unit)', () => {
  it('should instantiate and expose title signal', () => {
    const app = new App();
    expect(app).toBeTruthy();
    expect(app.title()).toBe('TodoApp');
  });
});
