import React, { Component } from 'react';
import { Loading } from './common';
import { Grid, Row, Col, Button, Label } from 'react-bootstrap';
import SalesAccountItem from './SalesAccountItem';
import BooleanSwitchModal from './BooleanSwitchModal';
import ListSelectItemModal from './ListSelectItemModal';
import Controls from './Controls';
import { formatDate } from '../helpers/dates';
import SalesAccountAddress from './SalesAccountAddress';

class SalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        const { loading,  dirty, saving, 
            hideEditModal, closeAccount, saveAccountUpdate,
            salesAccount, discountSchemeName, turnoverBandName, discountSchemes, turnoverBands,
            editDiscountScheme, setDiscountScheme, 
            editTurnoverBand, setTurnoverBand, 
            editEligibleForGoodCreditDiscount, setEligibleForGoodCreditDiscount,
            editGrowthPartner, setGrowthPartner,
            editEligibleForRebate, setEligibleForRebate,
            editDiscountSchemeVisible, editTurnoverBandVisible, editGoodCreditVisible, editGrowthPartnerVisible, editEligibleForRebateVisible,
            showConfirmCloseModal, hideConfirmCloseModal
        } = this.props;

        if (loading || !salesAccount) {
            return (<Loading/>);
        }

        return (
            <div>
                <Grid fluid={false}>
                    <Row>
                        <Col xs={10}>
                            <Row>
                                <Col sm={10}>
                                    <h2>{salesAccount.name}</h2>
                                </Col>
                            </Row>
                            <br />
                            <SalesAccountItem title={'Discount Scheme:'} value={discountSchemeName || 'select discount scheme'} handleClick={editDiscountScheme} />
                            { turnoverBands && <SalesAccountItem title={'Turnover Band:'} value={turnoverBandName || 'select turnover band'} handleClick={editTurnoverBand} />}
                            <SalesAccountItem
                                title={'Eligible For Good Credit:'}
                                value={salesAccount.eligibleForGoodCreditDiscount ? <Label bsStyle="success">Yes</Label> : <Label bsStyle="default">No</Label>}
                                handleClick={editEligibleForGoodCreditDiscount}
                            />
                            <SalesAccountItem
                                title={'Growth Partner:'}
                                value={salesAccount.growthPartner ? <Label bsStyle="success">Yes</Label> : <Label bsStyle="default">No</Label>}
                                handleClick={editGrowthPartner}
                            />
                            <SalesAccountItem
                                title={'Eligible for Rebate:'}
                                value={salesAccount.eligibleForRebate ? <Label bsStyle="success">Yes</Label> : <Label bsStyle="default">No</Label>}
                                handleClick={editEligibleForRebate}
                            />                            
                            <SalesAccountItem
                                title={'Address:'}
                                value={<SalesAccountAddress address={salesAccount.address}/>}
                                displayOnly
                            />
                            {salesAccount.closedOn && <SalesAccountItem title={'Account Closed:'} value={formatDate(salesAccount.closedOn)} displayOnly />}
                            <br />
                        </Col>
                    </Row >
                    <Controls 
                        closedOn={salesAccount.closedOn} dirty={dirty} saving={saving} salesAccount={salesAccount} 
                        closeAccount={closeAccount} saveAccountUpdate={saveAccountUpdate} 
                    />
                </Grid>

                <ListSelectItemModal
                    visible={editDiscountSchemeVisible} title={'Select Discounting Scheme'} items={discountSchemes}
                    currentItemUri={salesAccount.discountSchemeUri} hideModal={hideEditModal} setItem={setDiscountScheme}
                />
                <ListSelectItemModal
                    visible={editTurnoverBandVisible} title={'Select Turnover Band'} items={turnoverBands || []}
                    currentItemUri={salesAccount.turnoverBandUri} hideModal={hideEditModal} setItem={setTurnoverBand}
                />
                <BooleanSwitchModal 
                    visible={editGoodCreditVisible} title={'Eligible for Good Credit?'} trueText={'Yes'} falseText={'No'} 
                    current={salesAccount.eligibleForGoodCreditDiscount} hideModal={hideEditModal} setValue={setEligibleForGoodCreditDiscount}
                />
                <BooleanSwitchModal 
                    visible={editGrowthPartnerVisible} title={'Growth Partner?'} trueText={'Yes'} falseText={'No'} 
                    current={salesAccount.growthPartner} hideModal={hideEditModal} setValue={setGrowthPartner}
                />
                <BooleanSwitchModal 
                    visible={editEligibleForRebateVisible} title={'Eligible for Rebate?'} trueText={'Yes'} falseText={'No'} 
                    current={salesAccount.eligibleForRebate} hideModal={hideEditModal} setValue={setEligibleForRebate}
                />
            </div>
        );
    }
}

export default SalesAccount;