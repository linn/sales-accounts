import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col } from 'react-bootstrap';

class ProposalItem extends Component {
    render() {
        const { item } = this.props;

        return (
                <ListGroupItem>
                    <Row>
                        <Col xs={2}>{item.salesAccountUri}</Col>
                        <Col xs={2}>{item.calculatedTurnoverBandUri}</Col>
                        <Col xs={2}>{item.salesValueCurrency}</Col>
                        <Col xs={2}>{item.proposedTurnoverBandUri}</Col>
                    </Row>
                </ListGroupItem>
        );
    }
}

export default ProposalItem;
