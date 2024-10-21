# Vacation Request App

## Overview

The Vacation Request App is a web application that allows users to submit vacation requests, view their current requests, and manage their vacation days. It consists of a React front-end that communicates with a .NET Core back-end API.

## Features

- Submit vacation requests with start and end dates.
- View a list of submitted vacation requests.
- Filter and sort vacation requests by various criteria.
- Calculate vacation days automatically based on selected dates.

## Technologies Used

- **Frontend**: React, Material-UI, Axios
- **Backend**: .NET Core, Entity Framework Core
- **Database**: Entity Framework Core's in-memory database provider designed for testing purposes.

## Getting Started

### Prerequisites

Ensure you have the following installed:

- [Node.js](https://nodejs.org/) (v14 or later)
- [.NET Core SDK](https://dotnet.microsoft.com/download) (v5 or later)
- A code editor (e.g., [Visual Studio Code](https://code.visualstudio.com/), [Visual Studio 2022](https://visualstudio.microsoft.com/vs/))

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.

The page will reload when you make changes.\
You may also see any lint errors in the console.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can't go back!**

If you aren't satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you're on your own.

You don't have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn't feel obligated to use this feature. However we understand that this tool wouldn't be useful if you couldn't customize it when you are ready for it.


