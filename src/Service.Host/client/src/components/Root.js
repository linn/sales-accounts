import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { ConnectedRouter as Router } from 'react-router-redux';
import { OidcProvider } from 'redux-oidc';
import history from '../history';
import Navigation from './Navigation';
import App from './App';
import SalesAccount from '../containers/SalesAccount';
import TurnoverBandProposal from '../containers/TurnoverBandProposal';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';

class Root extends Component {
    render() {
        const { store } = this.props;

        return (
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router history={history}>
                        <div>
                            <Navigation />
                            <Route path="/" render={() => { document.title = 'Sales Accounts'; return false; }} />
                            <Route exact path="/" render={() => <Redirect to="/sales/accounts" />} />                            
                            <Route exact path="/sales" render={() => <Redirect to="/sales/accounts" />} />
                            <Route exact path="/sales/accounts" component={App} />                            
                            <Switch>
                                <Route exact path="/sales/accounts/turnover-band-proposals" component={TurnoverBandProposal} />
                                <Route exact path="/sales/accounts/signin-oidc-client" component={Callback} />
                                <Route exact path="/sales/accounts/:salesAccountId" component={SalesAccount} />
                            </Switch>
                        </div>
                    </Router>
                </OidcProvider>
            </Provider>
        );
    }
}

export default Root;