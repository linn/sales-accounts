import React, { Component } from 'react';
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

    handleSetItem(uri) {
        let a = uri;
    }

    render() {
        const { proposalItem, salesAccount, discountSchemes, turnoverBandSets } = this.props;
        const turnoverBands = getTurnoverBands({ item: salesAccount }, turnoverBandSets, discountSchemes);
        const displayOnly = false;
        const currentTurnover = getTurnoverBandName(turnoverBandSets, proposalItem.proposedTurnoverBandUri);
        const styles = {
            title: {
                textAlign: 'right',
                marginBottom: '6px'
            },
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
                                ? currentTurnover 
                                : <Button bsStyle="link" style={styles.button} onClick={() => this.handleShowModal()}>{currentTurnover}</Button>
                       }</Col>
                    </Row>
                </ListGroupItem>
                <ListSelectItemModal
                    visible={this.state.editBand} title={'Select Turnover Band'} items={turnoverBands || []}
                    currentItemUri={proposalItem.proposedTurnoverBandUri} hideModal={() => this.handleCloseModal()} setItem={this.handleSetItem}
                />
            </React.Fragment>
        );
    }
}

export default ProposalItem;
