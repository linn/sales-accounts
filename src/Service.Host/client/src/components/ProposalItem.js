import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col, OverlayTrigger, Tooltip } from 'react-bootstrap';
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
        const displayOnly = proposalItem.appliedToAccount || !proposalItem.includeInUpdate;
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
                    <Row className={proposalItem.includeInUpdate ? '' : 'text-muted'}>
                        <Col xs={3}>
                            <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip">{`Account id ${getSalesAccountId(salesAccount)}`}</Tooltip>}>
                                <a href={getSalesAccountId(salesAccount)}> {getSalesAccountName(salesAccount)}</a>
                            </OverlayTrigger>
                        </Col>
                        <Col xs={2}>{getDiscountSchemeName(salesAccount, discountSchemes)}</Col>
                        <Col xs={2}>{getSalesAccountTurnoverBandName(salesAccount, turnoverBandSets)}</Col>
                        <Col xs={2}>{proposalItem.salesValueCurrency}</Col>
                        <Col xs={2}>{
                            displayOnly
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip1">{proposalItem.appliedToAccount ? `${currentTurnoverBandName} has been applied to ${getSalesAccountName(salesAccount)}.` : `${getSalesAccountName(salesAccount)} has been excluded from proposal`}</Tooltip>}>
                                        <span>{currentTurnoverBandName}</span>
                                  </OverlayTrigger>
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
