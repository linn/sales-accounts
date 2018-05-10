﻿import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col } from 'react-bootstrap';
import { getSalesAccountName, getSalesAccountId, getDiscountSchemeName, getSalesAccountTurnoverBandName, getTurnoverBands } from '../selectors/salesAccountSelectors';
import { getTurnoverBandName } from '../selectors/turnoverBandSetSelectors';
import ListSelectItemModal from './ListSelectItemModal';

class ProposalItem extends Component {
    constructor() {
        super();
        this.state = {
            editBand : false
        }
    }

    handleShowModal() {
        this.setState({ editBand: true });
    }

    handleCloseModal() {
        this.setState({ editBand: false });
    }

    makeSetItemHandler() {
        const { proposalItem, updateProposedTurnoverBand } = this.props;
        return (turnoverBandUri) => updateProposedTurnoverBand(proposalItem.uri, turnoverBandUri);
    }

    render() {
        const { proposalItem, salesAccount, discountSchemes, turnoverBandSets } = this.props;
        const turnoverBands = getTurnoverBands(salesAccount, turnoverBandSets, discountSchemes);
        const displayOnly = false;
        const currentTurnoverBandName = getTurnoverBandName(turnoverBandSets, proposalItem.proposedTurnoverBandUri);
        const styles = {
            button: {
                padding: '0',
                outline: 0
            }
        }

        return (
            <React.Fragment>
                <ListGroupItem>
                    <Row>
                        <Col xs={3}>{getSalesAccountId(salesAccount)} {getSalesAccountName(salesAccount)}</Col>
                        <Col xs={2}>{getDiscountSchemeName(salesAccount, discountSchemes)}</Col>
                        <Col xs={2}>{getSalesAccountTurnoverBandName(salesAccount, turnoverBandSets)}</Col>
                        <Col xs={2}>{proposalItem.salesValueCurrency}</Col>
                        <Col onClick={() => this.handleShowModal()} xs={2}>{
                            displayOnly
                                ? currentTurnoverBandName 
                                : <Button bsStyle="link" style={styles.button} onClick={() => this.handleShowModal()}>{currentTurnoverBandName}</Button>
                       }</Col>
                    </Row>
                </ListGroupItem>
                <ListSelectItemModal
                    visible={this.state.editBand} title={'Select Turnover Band'} items={turnoverBands || []}
                    currentItemUri={proposalItem.proposedTurnoverBandUri} hideModal={() => this.handleCloseModal()} setItem={this.makeSetItemHandler()}
                />
            </React.Fragment>
        );
    }
}

export default ProposalItem;
