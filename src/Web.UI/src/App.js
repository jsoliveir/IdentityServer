import logo from './logo.svg';
import './App.css';
import {getAccessToken} from './Components/Authorize';
function App() {
  return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>You've been authorized to load this page!</p>
          {/* <p style="font-size:7px">{getAccessToken()}</p> */}
        </header>
      </div>
  );
}

export default App;
