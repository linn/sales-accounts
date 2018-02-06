import React, { Component } from 'react';
import { Loading } from './common';
import { Link } from 'react-router-dom';
import { Grid, Row, Col, Button } from 'react-bootstrap';
import SalesAccountItem from './SalesAccountItem';
import SalesAccountEditModal from './SalesAccountEditModal';

const styles = {
    item: {
        textAlign: 'right',
        marginBottom: '6px'
    }
}

class SalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        const { loading, salesAccount, discountSchemeName, turnoverBandName, salesAccountEditModal, ...props } = this.props;

        if (loading || !salesAccount) {
            return (<div>Loading</div>);
        }
        console.log(turnoverBandName);
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
                            <SalesAccountItem title={'Discount Scheme:'} value={discountSchemeName} {...props}/>
                            <SalesAccountItem title={'Turnover Band:'} value={turnoverBandName} {...props}/>
                            <SalesAccountItem title={'Eligible For Good Credit:'} value={salesAccount.eligibleForGoodCreditDiscount.toString()} {...props}/>
                            <SalesAccountItem title={'Account Closed:'} value={salesAccount.closedOn} {...props}/>
                            <br />
                            <Row>
                                <Col sm={2}>
                                    <Link to="/sales/accounts">Back</Link>
                                </Col>
                            </Row>
                        </Col>
                    </Row >
                </Grid>
                <SalesAccountEditModal visible={salesAccountEditModal.visible} {...props}/>
            </div>
        );
    }
}

export default SalesAccount;