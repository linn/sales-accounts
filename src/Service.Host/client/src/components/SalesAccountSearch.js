import React, { Component } from 'react';
import { FormGroup, FormControl, ControlLabel, Button, Modal, ListGroup, ListGroupItem, Label, OverlayTrigger, Popover } from 'react-bootstrap';
import { Loading } from './common';
import { debounce } from '../helpers/utilities';

let timeoutId;

class SalesAccountSearch extends Component {
    state = { searchTerm: '' }

    render() {
        const { salesAccounts, loading } = this.props;

        return (
            <div>
            <h2>Select Sales Account</h2 >
                    <div>
                        <FormGroup>
                            <ControlLabel>Name or Account Id</ControlLabel>
                            <FormControl autoFocus value={this.state.searchTerm} onChange={e => this.handleSearchTermChange(e)} type="text" placeholder="Enter account name or account id" ></FormControl>
                        </FormGroup>
                        {salesAccounts.length > 0
                            ? (
                                <ListGroup>
                                    {salesAccounts.map((salesAccount, i) => (
                                        <ListGroupItem key={i} onClick={() => this.handleSalesAccountClick(salesAccount)}>{salesAccount.name} <Label className="pull-right" bsStyle="primary">{salesAccount.accountId}</Label></ListGroupItem>
                                    ))}
                                </ListGroup>
                            )
                            : loading
                                ? <Loading />
                                : <span>No matching accounts</span>
                        }
                </div>   </div>
                     );
    }

    handleSearchTermChange(e) {
        const { searchSalesAccounts } = this.props;
        const searchTerm = e.target.value;

        this.setState({ searchTerm });

        if (timeoutId) {
            clearTimeout(timeoutId);
        }

        timeoutId = setTimeout(() => searchSalesAccounts(searchTerm), 500);
    }

    handleSalesAccountClick(salesAccount) {
        const { onSelect, fetchSalesAccount } = this.props;

        // fetchSalesAccount(salesAccount.links.find(a => a.rel === 'self').href);
        onSelect(salesAccount);
    }
}

export default SalesAccountSearch;