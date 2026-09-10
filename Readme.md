# Execution Steps

This repository contains two runnable projects: the Angular frontend (`TodoApp`) and the .NET backend API (`TodoAppApi`). Run the following commands from the repository root.

Frontend (Angular — TodoApp)

1. Install dependencies and start the dev server:

```bash
cd TodoApp
npm install
npm start
```

2. Run frontend unit tests:

```bash
cd TodoApp
npm test
```

Optional: run Vitest directly:

```bash
cd TodoApp
npx vitest run --run
```

Backend (C# .NET 9 — TodoAppApi)

1. Restore and run the API:

```bash
cd TodoAppApi/TodoAppApi
dotnet restore
dotnet run
```

The API listens on the `applicationUrl` defined in [TodoAppApi/TodoAppApi/Properties/launchSettings.json](TodoAppApi/TodoAppApi/Properties/launchSettings.json) (default: `http://localhost:5029`). Update `TodoApp/src/environments/environment.ts` if you change the backend URL.

2. Run backend tests:

```bash
cd TodoAppApi/TodoAppApiTest
dotnet test
```

