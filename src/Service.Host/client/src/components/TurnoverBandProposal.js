import React, { Component } from 'react';
import { Loading } from './common';
import { Grid, Row, Col, Button, ListGroup, ListGroupItem, Alert } from 'react-bootstrap';
import { formatDate } from '../helpers/dates';
import ProposalItem from './ProposalItem';
import { getSalesAccount } from '../selectors/salesAccountsSelectors';

class TurnoverBandProposal extends Component {
    render() {
        const { financialYear, loading, proposedTurnoverBands, salesAccounts } = this.props;

        if (loading) {
            return (<Loading />);
        }

        return (
            <div>
                <Grid>
                    <Row>
                        <Col xs={10}>
                            <h3>Turnover band proposal using sales values for {financialYear}</h3>
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={12}>
                            {proposedTurnoverBands && proposedTurnoverBands.length > 0
                                ?
                                <div>
                                <ListGroupItem>
                                    <Row>
                                        <Col xs={2}>Sales Account</Col>
                                        <Col xs={2}>Current</Col>
                                        <Col xs={2}>Sales Value</Col>
                                        <Col xs={2}>Proposed</Col>
                                    </Row>
                                </ListGroupItem>
                                <ListGroup>
                                    {proposedTurnoverBands.map((item) => (<ProposalItem item={item} key={item.uri} salesAccount={getSalesAccount(salesAccounts, item.salesAccountUri)}/>))}
                                </ListGroup>
                                </div>
                                : <Alert bsStyle="warning">No turnover band proposals created</Alert>
                            }
                        </Col>
                    </Row>
                </Grid>
            </div>
        );
    }
}

export default TurnoverBandProposal;