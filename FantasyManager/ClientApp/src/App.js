import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Teams } from './components/Teams';
import { Players } from './components/Players';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/players' component={Players} />
        <Route path='/teams' component={Teams} />
      </Layout>
    );
  }
}
