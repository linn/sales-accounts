import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col } from 'react-bootstrap';
import { getSalesAccountName, getDiscountSchemeName, getSalesAccountTurnoverBandName } from '../selectors/salesAccountSelectors';

class ProposalItem extends Component {
    render() {
        const { proposalItem, salesAccount, discountSchemes, turnoverBandSets } = this.props;

        return (
                <ListGroupItem>
                    <Row>
                        <Col xs={2}>{getSalesAccountName(salesAccount)}</Col>
                        <Col xs={2}>{getDiscountSchemeName(salesAccount, discountSchemes)}</Col>
                        <Col xs={2}>{getSalesAccountTurnoverBandName(salesAccount, turnoverBandSets)}</Col>
                        <Col xs={2}>{proposalItem.salesValueCurrency}</Col>
                        <Col xs={2}>{proposalItem.proposedTurnoverBandUri}</Col>
                    </Row>
                </ListGroupItem>
        );
    }
}

export default ProposalItem;
