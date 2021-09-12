import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import Authorize from './Components/Authorize';

ReactDOM.render(
  <React.StrictMode>
    <Authorize authority="http://localhost:5000" scopes="weather.read">
      <App />
    </Authorize>
  </React.StrictMode>,
  document.getElementById('root')
);
