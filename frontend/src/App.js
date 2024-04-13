import React from 'react';
import Navbar from './components/Navbar';
import './App.css';
import Home from './components/pages/Home';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import ArtifactsList from './components/ArtifactsList';
import ArtStylesList from './components/ArtstylesList';
import ExhibitionsList from './components/ExhibitionsList';

function App() {
  return (
    <>
      <Router>
        <Navbar />
        <Switch>
          <Route path='/' exact component={Home} />
          <Route path='/artifact-card' component={ArtifactsList} />
          <Route path='/art-style' component={ArtStylesList} />
          <Route path='/exhibition' component={ExhibitionsList} />
        </Switch>
      </Router>
    </>
  );
}

export default App;
