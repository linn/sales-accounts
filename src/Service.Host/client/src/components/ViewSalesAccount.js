import React, { Component } from 'react';
import { Loading } from './common';
import { Link } from 'react-router-dom'

class ViewSalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        return (
            <div>
                <h1> Look at this sales acccount</h1>
                <Link to="/sales/accounts">Back</Link>
            </div>
        );
    }
}

export default ViewSalesAccount;