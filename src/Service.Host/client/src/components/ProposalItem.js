import React, { Component } from 'react';
import { ListGroupItem, Button, Row, Col, OverlayTrigger, Tooltip, Glyphicon } from 'react-bootstrap';
import ListSelectItemModal from './ListSelectItemModal';
import { formatWithCommas } from '../helpers/utilities';

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
        const { 
            proposalItem, currentTurnoverBandName, calculatedTurnoverBandName, 
            salesAccountTurnoverBandName, salesAccountId, salesAccountName, 
            discountSchemeName, displayOnly, 
            updateProposedTurnoverBand, excludeProposedTurnoverBand,
            turnoverBands
        } = this.props;

        const styles = {
            button: {
                padding: '0',
                outline: 0
            },
            column: proposalItem.includeInUpdate ? {} : { 'text-decoration': 'line-through', 'color': 'lightgray' },
            alignright: { 'text-align': 'right' }
        }

        return (
            <React.Fragment >
                <ListGroupItem>
                    <Row>
                        <Col style={styles.column} xs={3}>
                            <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip">{`Account id ${salesAccountId}`}</Tooltip>}>
                                <a href={salesAccountId}> {salesAccountName}</a>
                            </OverlayTrigger>
                        </Col>
                        <Col style={styles.column} xs={2}>{discountSchemeName}</Col>
                        <Col style={styles.column} xs={2}>{salesAccountTurnoverBandName}</Col>
                        <Col style={styles.column, styles.alignright} xs={1}>
                            <OverlayTrigger placement="top" overlay={<Tooltip id="currency-tooltip">{proposalItem.currencyCode ? proposalItem.currencyCode : 'No Sales'}</Tooltip>}>
                                <span> {formatWithCommas(proposalItem.salesValueCurrency, 0)} </span> 
                            </OverlayTrigger>
                        </Col>
                        <Col style={styles.column} xs={2}>{
                            displayOnly
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip1">{proposalItem.appliedToAccount ? `${currentTurnoverBandName} has been applied to ${salesAccountName}.` : `${salesAccountName} has been excluded from proposal`}</Tooltip>}>
                                    <span>{currentTurnoverBandName}</span>
                                </OverlayTrigger>
                                : <Button bsStyle="link" style={styles.button} onClick={() => this.handleShowModal()}>{currentTurnoverBandName}</Button>
                        }</Col>
                        <Col xs={1}>{
                            proposalItem.calculatedTurnoverBandUri !== proposalItem.proposedTurnoverBandUri && !displayOnly
                                ? <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip2">{`Revert back to calulated turnover band of ${calculatedTurnoverBandName}`}</Tooltip>}>
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
