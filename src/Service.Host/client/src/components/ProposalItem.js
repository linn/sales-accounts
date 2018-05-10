import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col, OverlayTrigger, Tooltip, Glyphicon } from 'react-bootstrap';
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

    handleExclude() {
        const { proposalItem, excludeProposedTurnoverBand } = this.props;
        excludeProposedTurnoverBand(proposalItem.uri);
    }

    handleInclude() {
        const { proposalItem, updateProposedTurnoverBand } = this.props;
        updateProposedTurnoverBand(proposalItem.uri, proposalItem.proposedTurnoverBandUri);
    }

    handleRevert() {
        const { proposalItem, updateProposedTurnoverBand } = this.props;
        updateProposedTurnoverBand(proposalItem.uri, proposalItem.calculatedTurnoverBandUri);
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
                        <Col xs={1}>{proposalItem.salesValueCurrency}</Col>
                        <Col xs={2}>{
                            displayOnly
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip1">{proposalItem.appliedToAccount ? `${currentTurnoverBandName} has been applied to ${getSalesAccountName(salesAccount)}.` : `${getSalesAccountName(salesAccount)} has been excluded from proposal`}</Tooltip>}>
                                    <span>{currentTurnoverBandName}</span>
                                </OverlayTrigger>
                                : <Button bsStyle="link" style={styles.button} onClick={() => this.handleShowModal()}>{currentTurnoverBandName}</Button>
                        }</Col>
                        <Col xs={1}>{
                            proposalItem.calculatedTurnoverBandUri !== proposalItem.proposedTurnoverBandUri && !displayOnly
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip2">{`Revert back to calulated turnover band of ${getTurnoverBandName(turnoverBandSets, proposalItem.calculatedTurnoverBandUri)}`}</Tooltip>}>
                                    <Button bsSize="small" onClick={() => this.handleRevert()}>
                                        <Glyphicon glyph="refresh" />
                                    </Button>
                                </OverlayTrigger>
                                : '' }   
                        </Col>
                        <Col xs={1}>{  
                            !proposalItem.includeInUpdate
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip3">Reinclude in proposal</Tooltip>}>
                                    <Button className="muted" bsStyle="success" bsSize="small" onClick={() => this.handleInclude()}>
                                          <Glyphicon glyph="plus" />
                                      </Button>
                                  </OverlayTrigger>
                                : '' }
                            {!displayOnly
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip4">Remove from proposal</Tooltip>}>
                                    <Button className="muted" bsStyle="danger" bsSize="small" onClick={() => this.handleExclude()}>
                                          <Glyphicon glyph="remove" />
                                      </Button>
                                  </OverlayTrigger> : ''}
                        </Col>
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
