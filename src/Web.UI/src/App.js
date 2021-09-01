import Authorize from './Components/Authorize'
import { BrowserRouter as Router,Route,Switch } from 'react-router-dom'
import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <Router>
      <Switch>
        <Authorize login="/">
          <Route path="/auth"><div>OK</div></Route>
        </Authorize>
      </Switch>
    </Router>
  );
}

export default App;
