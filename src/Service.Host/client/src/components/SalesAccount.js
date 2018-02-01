import React, { Component } from 'react';
import { Loading } from './common';
import { Link } from 'react-router-dom'

class SalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        const { loading, salesAccount } = this.props;

        if (loading || !salesAccount) {
            return (<div>Loading</div>);
        }

        return (
            <div>
                <h1> Look at this sales acccount</h1>
                <h2>{salesAccount.name}</h2>
                <Link to="/sales/accounts">Back</Link>
            </div>
        );
    }
}

export default SalesAccount;