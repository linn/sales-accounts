import React, { Component } from 'react';
import { Loading } from './common';
import { Link } from 'react-router-dom';
import { Grid, Row, Col, Button, Label } from 'react-bootstrap';
import SalesAccountItem from './SalesAccountItem';
import SwitchModal from './SwitchModal';
import ListSelectItemModal from './ListSelectItemModal';
import Controls from './Controls';
import discountSchemes from '../reducers/discountSchemes';

const styles = {
    item: {
        textAlign: 'right',
        marginBottom: '6px'
    }
}

class SalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        const { loading, hideEditModal, salesAccount, discountSchemeName, turnoverBandName, 
            showTurnoverBandEditModal, showDiscountSchemeEditModal, editGoodCredit,
            setDiscountScheme, discountSchemes, turnoverBands, setTurnoverBand,
            editDiscountSchemeVisible, editTurnoverBandVisible, editGoodCreditVisible, ...props } = this.props;

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
                            <SalesAccountItem title={'Discount Scheme:'} value={discountSchemeName || 'select discount scheme'} handleClick={showDiscountSchemeEditModal} />
                            <SalesAccountItem title={'Turnover Band:'} value={turnoverBandName || 'select turnover band'} handleClick={showTurnoverBandEditModal} />
                            <SalesAccountItem 
                                title={'Eligible For Good Credit:'} 
                                value={salesAccount.eligibleForGoodCreditDiscount ? <Label bsStyle="success">Yes</Label> : <Label bsStyle="default">No</Label>}
                                handleClick={editGoodCredit}
                            />
                            {!salesAccount.closedOn &&  <SalesAccountItem title={'Account Closed:'} value={salesAccount.closedOn} />}
                            <br />
                        </Col>
                    </Row >
                    <Controls closedOn={salesAccount.closedOn} />
                </Grid>

                <ListSelectItemModal 
                    visible={editDiscountSchemeVisible} items={discountSchemes} 
                    currentItemUri={salesAccount.discountSchemeUri} hideModal={hideEditModal} setItem={setDiscountScheme}
                />
                <ListSelectItemModal 
                    visible={editTurnoverBandVisible} items={turnoverBands || []} 
                    currentItemUri={salesAccount.turnoverBandUri} hideModal={hideEditModal} setItem={setTurnoverBand}
                />
     
                {/* <TurnoverBandEditModal discountSchemeUri={salesAccount.discountSchemeUri} turnoverBandUri={salesAccount.turnoverBandUri}/> */}
                <SwitchModal visible={editGoodCreditVisible} hideModal={hideEditModal}/>
            </div>
        );
    }
}

export default SalesAccount;