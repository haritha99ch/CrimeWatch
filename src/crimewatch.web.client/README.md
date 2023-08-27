# Crime Watch React Frontend

This Standalone JavaScript React project was generated with [Visual Studio 2022](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-react?view=vs-2022) 17.8.0 preview 1.0.

## Overview

This project can serve as the frontend to an ASP.NET Web API Backend. See above link for details.

## Initial Setup

CrimeWatch/src/CrimeWatch.Web.Client >

```shell
npm install
```

## Development Preview

CrimeWatch/src/CrimeWatch.Web.Client >

```bash
npm run dev
```

## Problems & Workarounds

- `npm run build` Error: ENOENT: no such file or directory, open 'C:\Users\username\AppData\Roaming\ASP.NET\https\CrimeWatch.Web.Client.key'
  - Details: running `npm run build` without initially running `npm run dev` will not create certificate `dotnet dev-cert`.
  - Workaround: Updated `package.json` to run `node aspnetcore-https.js` before the build script where certificate is being created.

```json
"scripts": {
 "dev": "vite",
 "build": "node aspnetcore-https.js && vite build",
},
```
