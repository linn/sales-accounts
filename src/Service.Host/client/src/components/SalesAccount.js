import React, { Component } from 'react';
import { Loading } from './common';
import { Link } from 'react-router-dom';
import { Grid, Row, Col, Button, Label } from 'react-bootstrap';
import SalesAccountItem from './SalesAccountItem';
import SwitchModal from './SwitchModal';
import ListSelectItemModal from './ListSelectItemModal';
import Controls from './Controls';
import discountSchemes from '../reducers/discountSchemes';

class SalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        const { loading, hideEditModal, 
            salesAccount, discountSchemeName, turnoverBandName, discountSchemes, turnoverBands,
            editDiscountScheme, setDiscountScheme, editTurnoverBand, setTurnoverBand, editEligibleForGoodCreditDiscount, setEligibleForGoodCreditDiscount,
            editDiscountSchemeVisible, editTurnoverBandVisible, editGoodCreditVisible } = this.props;

        if (loading || !salesAccount) {
            return (<div>Loading</div>);
        }

        return (
            <div>
                <Grid fluid={false}>
                    <Row>
                        <Col xs={8}>
                            <Row>
                                <Col sm={2}>
                                    <h2>{salesAccount.name}</h2>
                                </Col>
                            </Row>
                            <br />
                            <SalesAccountItem title={'Discount Scheme:'} value={discountSchemeName || 'select discount scheme'} handleClick={editDiscountScheme} />
                            <SalesAccountItem title={'Turnover Band:'} value={turnoverBandName || 'select turnover band'} handleClick={editTurnoverBand} />
                            <SalesAccountItem
                                title={'Eligible For Good Credit:'}
                                value={salesAccount.eligibleForGoodCreditDiscount ? <Label bsStyle="success">Yes</Label> : <Label bsStyle="default">No</Label>}
                                handleClick={editEligibleForGoodCreditDiscount}
                            />
                            {!salesAccount.closedOn && <SalesAccountItem title={'Account Closed:'} value={salesAccount.closedOn} />}
                            <br />
                        </Col>
                    </Row >
                    <Controls closedOn={salesAccount.closedOn} />
                </Grid>

                <ListSelectItemModal
                    visible={editDiscountSchemeVisible} title={'Select Discounting Scheme'} items={discountSchemes}
                    currentItemUri={salesAccount.discountSchemeUri} hideModal={hideEditModal} setItem={setDiscountScheme}
                />
                <ListSelectItemModal
                    visible={editTurnoverBandVisible} title={'select Turnover Band'} items={turnoverBands || []}
                    currentItemUri={salesAccount.turnoverBandUri} hideModal={hideEditModal} setItem={setTurnoverBand}
                />
                <SwitchModal visible={editGoodCreditVisible} title={'Eligible for Good Credit?'} 
                    value1={'Yes'} value2={'No'} current={salesAccount.eligibleForGoodCreditDiscount} hideModal={hideEditModal} setValue={setEligibleForGoodCreditDiscount} 
                />
            </div>
        );
    }
}

export default SalesAccount;