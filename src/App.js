import React, { useState } from 'react';
import './App.css';

function App() {
  const [city, setCity] = useState('');
  const [weather, setWeather] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const fetchWeather = async () => {
    if (!city.trim()) {
      setError('Please enter a city name');
      return;
    }

    setLoading(true);
    setError('');
    setWeather(null);

    try {
      const response = await fetch(`http://localhost:5234/weatherforecast/${city}`);
      
      if (!response.ok) {
        throw new Error('City not found');
      }

      const data = await response.json();
      setWeather(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    fetchWeather();
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Quick Weather App</h1>
        
        <form onSubmit={handleSubmit} className="search-form">
          <input
            type="text"
            value={city}
            onChange={(e) => setCity(e.target.value)}
            placeholder="Enter city name..."
            className="city-input"
          />
          <button type="submit" disabled={loading} className="search-button">
            {loading ? 'Loading...' : 'Get Weather'}
          </button>
        </form>

        {error && <div className="error">{error}</div>}

        {weather && (
          <div className="weather-card">
            <h2>{weather.city}, {weather.country}</h2>
            
            <div className="temperature-section">
              <div className="temp-display">
                <span className="temp-c">{weather.temperatureCelsius}°C</span>
                <span className="temp-divider">|</span>
                <span className="temp-f">{weather.temperatureFahrenheit}°F</span>
              </div>
              <p className="description">{weather.description}</p>
            </div>

            <div className="details-grid">
              <div className="detail-item">
                <span className="label">Feels like:</span>
                <span className="value">{weather.feelsLikeCelsius}°C / {weather.feelsLikeFahrenheit}°F</span>
              </div>
              
              <div className="detail-item">
                <span className="label">Humidity:</span>
                <span className="value">{weather.humidity}%</span>
              </div>
              
              <div className="detail-item">
                <span className="label">Wind speed:</span>
                <span className="value">{weather.windSpeed} m/s</span>
              </div>
            </div>
          </div>
        )}
      </header>
    </div>
  );
}

export default App;