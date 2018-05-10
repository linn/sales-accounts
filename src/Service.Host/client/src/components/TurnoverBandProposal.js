import React, { Component } from 'react';
import { Loading } from './common';
import { Grid, Row, Col, Button, ListGroup, ListGroupItem, Alert } from 'react-bootstrap';
import { formatDate } from '../helpers/dates';
import ProposalItem from './ProposalItem';
import { getSalesAccount } from '../selectors/salesAccountsSelectors';

class TurnoverBandProposal extends Component {
    render() {
        const {
            turnoverBandProposal,
            financialYear,
            loading,
            proposedTurnoverBands,
            salesAccounts,
            discountSchemes,
            turnoverBandSets,
            calculateTurnoverBandProposal,
            updateProposedTurnoverBand,
            applyTurnoverBandProposal
        } = this.props;

        if (loading) {
            return (<Loading />);
        }

        return (
            <div>
                <Grid>
                    <Row>
                        <Col xs={8}>
                            <h4>Turnover band proposal using sales for {financialYear}</h4>
                        </Col>
                        <Col xs={4}>
                            <Button className="pull-right" onClick={() => calculateTurnoverBandProposal()} >Recalculate Proposals</Button>
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={12}>
                            {proposedTurnoverBands && proposedTurnoverBands.length > 0
                                ?
                                <div>
                                <ListGroupItem>
                                    <Row><b>
                                        <Col xs={3}>Sales Account</Col>
                                        <Col xs={2}>Discount Scheme</Col>
                                        <Col xs={2}>Current</Col>
                                        <Col xs={2}>Sales Value</Col>
                                        <Col xs={2}>Proposed</Col>
                                    </b></Row>
                                </ListGroupItem>
                                <ListGroup>
                                        {proposedTurnoverBands.map((proposalItem) => (
                                            <ProposalItem
                                                proposalItem={proposalItem}
                                                key={proposalItem.uri}
                                                salesAccount={getSalesAccount(salesAccounts, proposalItem.salesAccountUri)}
                                                discountSchemes={discountSchemes}
                                                turnoverBandSets={turnoverBandSets}
                                                updateProposedTurnoverBand={updateProposedTurnoverBand}
                                            />))}
                                </ListGroup>
                                </div>
                                : <Alert bsStyle="warning">No turnover band proposals created</Alert>
                            }
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={8}>
                        </Col>
                        <Col xs={4}>
                            <Button className="pull-right" onClick={() => applyTurnoverBandProposal(turnoverBandProposal.applyUri, financialYear)} >Apply Proposal To Accounts</Button>
                        </Col>
                    </Row>
                </Grid>
            </div>
        );
    }
}

export default TurnoverBandProposal;