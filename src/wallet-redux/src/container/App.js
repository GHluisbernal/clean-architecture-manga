
import React, { Component } from 'react';
import {
  BrowserRouter,
  Route,
  Switch
} from 'react-router-dom';

import PrivateRoute from './privateRoute'
import LoginPage from '../features/loginPage/LoginPage';
import DashboardPage from '../features/dashboardPage/DashboardPage';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div>
          <Switch>
            <Route path='/' exact={true} component={LoginPage} />
            <Route path='/login' component={LoginPage} />
            <PrivateRoute path='/dashboard' component={DashboardPage} />
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
