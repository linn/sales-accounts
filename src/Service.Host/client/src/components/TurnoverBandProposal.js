import React, { Component } from 'react';
import { Loading } from './common';
import { Grid, Row, Col, Button, ListGroup, ListGroupItem, Alert, OverlayTrigger, Tooltip, Glyphicon } from 'react-bootstrap';
import { formatDate } from '../helpers/dates';
import ProposalItem from './ProposalItem';
import { getSalesAccount } from '../selectors/salesAccountsSelectors';
import YesNoModal from './YesNoModal';
import config from '../config';

class TurnoverBandProposal extends Component {
    constructor() {
        super();
        this.state = {
            showRecalculateYesNoModal: false,
            showApplyYesNoModal: false
        }
    }

    handleShowRecalculateYesNoModal() {
        this.setState({ showRecalculateYesNoModal: true });
    }

    handleCloseRecalculateYesNoModal() {
        this.setState({ showRecalculateYesNoModal: false });
    }

    handleShowApplyYesNoModal() {
        this.setState({ showApplyYesNoModal: true });
    }

    handleCloseApplyYesNoModal() {
        this.setState({ showApplyYesNoModal: false });
    }

    makeApplyHandler() {
        const { turnoverBandProposal, financialYear, applyTurnoverBandProposal } = this.props;
        return () => applyTurnoverBandProposal(turnoverBandProposal.applyUri, financialYear);
    }

    render() {
        const {
            financialYear,
            loading,
            proposedTurnoverBands,
            salesAccounts,
            discountSchemes,
            turnoverBandSets,
            calculateTurnoverBandProposal,
            updateProposedTurnoverBand,
            excludeProposedTurnoverBand
        } = this.props;

        const styles = {
            alignright: { 'text-align': 'right' }
        }

        if (loading) {
            return (<Loading />);
        }

        return (
            <div >
                <Grid>
                    <Row>
                        <Col xs={8}>
                            <h4>Turnover band proposal using sales for {financialYear}</h4>
                        </Col>
                        <Col xs={2}>
                            <OverlayTrigger placement="top" overlay={<Tooltip id="tooltip1">Download turnover band proposal as a CSV file</Tooltip>}>
                                <Button href={`${config.appRoot}/sales/accounts/turnover-band-proposals/export`}><Glyphicon className="text-muted" glyph="export" /> Export</Button>
                            </OverlayTrigger>
                        </Col>
                        <Col xs={2}>
                            <Button className="pull-right" onClick={() => this.handleShowRecalculateYesNoModal()} >Recalculate Proposals</Button>
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
                                        <Col style={styles.alignright} xs={1}>Sales</Col>
                                        <Col xs={2}>Proposed</Col>
                                        <Col xs={2}></Col>
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
                                                excludeProposedTurnoverBand={excludeProposedTurnoverBand}
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
                            <Button className="pull-right" onClick={() => this.handleShowApplyYesNoModal()}>Apply Proposal To Accounts</Button>
                        </Col>
                    </Row>
                </Grid>
                <YesNoModal
                    visible={this.state.showRecalculateYesNoModal} title={'Are you sure?'}
                    yesButtonText={'Ok'} noButtonText={'Cancel'}
                    text={'Recalculating proposals will lose ALL unapplied changes.'}
                    instructionText={'Click OK only if you are sure you want to do this.'}
                    performYesAction={calculateTurnoverBandProposal}
                    hideModal={() => this.handleCloseRecalculateYesNoModal()}
                />
                <YesNoModal
                    visible={this.state.showApplyYesNoModal} title={'Are you sure?'}
                    yesButtonText={'Ok'} noButtonText={'Cancel'}
                    text={'Applying these proposals will update the turnover band for all selected accounts.'}
                    instructionText={'Click OK only if you are ready to go ahead with this.'}
                    performYesAction={this.makeApplyHandler()}
                    hideModal={() => this.handleCloseApplyYesNoModal()}
                />
            </div>
        );
    }
}

export default TurnoverBandProposal;