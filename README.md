# Weather App

A simple weather app that shows current weather for any city. This is my first project learning C# and React together!

## What it does

- Type in a city name and get the current weather
- Shows temperature in both Celsius and Fahrenheit  
- Displays weather description, humidity, and wind speed
- Built with a C# backend and React frontend

## Technologies Used

- **C#** - Backend API
- **React** - Frontend website
- **OpenWeatherMap API** - Gets the weather data

## How to run it

### What you need first
- .NET SDK (for the C# part)
- Node.js (for the React part)
- Free API key from OpenWeatherMap

### Steps
1. Clone this project to your computer
2. Get a free API key from [OpenWeatherMap](https://openweathermap.org/api)
3. Put your API key in the `Program.cs` file where it says `YOUR_API_KEY_HERE`

4. Start the backend:
   ```
   cd WeatherApi
   dotnet run
   ```

5. Start the frontend (in a new terminal):
   ```
   cd weather-frontend
   npm install
   npm start
   ```

6. Go to `http://localhost:3000` in your browser and try searching for a city!

## What I learned

This was my first time building a full-stack app! I learned:
- How to make a C# API that talks to other APIs
- How React components work and manage state
- How frontend and backend communicate with HTTP requests
- Basic error handling and user interface design

## Issues I ran into

- CORS errors when React tried to call the C# API (fixed by adding CORS configuration)
- Understanding how `useState` works in React
- Figuring out how to parse the OpenWeatherMap API response

---

This is a learning project - feel free to suggest improvements or ask questions!