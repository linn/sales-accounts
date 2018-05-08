import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col } from 'react-bootstrap';
import { getSalesAccountName, getSalesAccountId, getDiscountSchemeName, getSalesAccountTurnoverBandName } from '../selectors/salesAccountSelectors';
import { getTurnoverBandName } from '../selectors/turnoverBandSetSelectors';

class ProposalItem extends Component {
    render() {
        const { proposalItem, salesAccount, discountSchemes, turnoverBandSets } = this.props;

        return (
                <ListGroupItem>
                    <Row>
                        <Col xs={3}>{getSalesAccountId(salesAccount)} {getSalesAccountName(salesAccount)}</Col>
                        <Col xs={2}>{getDiscountSchemeName(salesAccount, discountSchemes)}</Col>
                        <Col xs={2}>{getSalesAccountTurnoverBandName(salesAccount, turnoverBandSets)}</Col>
                        <Col xs={2}>{proposalItem.salesValueCurrency}</Col>
                        <Col xs={2}>{getTurnoverBandName(turnoverBandSets, proposalItem.proposedTurnoverBandUri)}</Col>
                    </Row>
                </ListGroupItem>
        );
    }
}

export default ProposalItem;
