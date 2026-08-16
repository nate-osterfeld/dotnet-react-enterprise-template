import { useState, useEffect } from 'react';
import './App.css';

interface TodoItem {
  id: number;
  title: string;
  isCompleted: boolean;
}

function App() {
  const [todos, setTodos] = useState<TodoItem[]>([]);
  const [newTitle, setNewTitle] = useState('');

  // Fetch todos from the backend API on load
  const fetchTodos = async () => {
    try {
      const response = await fetch('/api/todos');
      if (response.ok) {
        const data = await response.json();
        setTodos(data);
      }
    } catch (error) {
      console.error('Error fetching todos:', error);
    }
  };

  useEffect(() => {
    fetchTodos();
  }, []);

  // Create a new todo item via POST request
  const addTodo = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newTitle.trim()) return;

    try {
      const response = await fetch('/api/todos', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: newTitle, isCompleted: false }),
      });

      if (response.ok) {
        setNewTitle('');
        fetchTodos(); // Refresh list
      }
    } catch (error) {
      console.error('Error creating todo:', error);
    }
  };

  return (
    <div style={{ maxWidth: '600px', margin: '40px auto', fontFamily: 'Arial' }}>
      <h1>Todo App</h1>

      <form onSubmit={addTodo} style={{ marginBottom: '20px' }}>
        <input
          type="text"
          placeholder="Enter a new todo..."
          value={newTitle}
          onChange={(e) => setNewTitle(e.target.value)}
          style={{ padding: '8px', width: '70%', marginRight: '10px' }}
        />
        <button type="submit" style={{ padding: '8px 16px' }}>Add Todo</button>
      </form>

      <ul>
        {todos.map((todo) => (
          <li key={todo.id} style={{ marginBottom: '8px' }}>
            {todo.title} {todo.isCompleted ? '✅' : '⏳'}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;