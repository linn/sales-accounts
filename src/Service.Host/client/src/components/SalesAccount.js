import React, { Component } from 'react';
import { Loading } from './common';
import { Link } from 'react-router-dom';
import { Grid, Row, Col, Button } from 'react-bootstrap';
import SalesAccountItem from './SalesAccountItem';
import SalesAccountEditModal from './SalesAccountEditModal';
import DiscountSchemeEditModal from '../containers/DiscountSchemeEditModal';
import Controls from './Controls';

const styles = {
    item: {
        textAlign: 'right',
        marginBottom: '6px'
    }
}

class SalesAccount extends Component {
    state = { searchTerm: '' }

    render() {
        const { loading, salesAccount, discountSchemeName, turnoverBandName, salesAccountEdit, showEditModal, editDiscountScheme, ...props } = this.props;

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
                            {/* <SalesAccountItem title={'Discount Scheme:'} value={discountSchemeName} {...props} /> */}
                            <Row>
                                <Col sm={4} style={styles.item}>
                                    <b>{'Discount Scheme:'}</b>
                                </Col>
                                <Col sm={2}>
                                    <Button bsStyle="link" style={{ padding: '0' }} onClick={() => showEditModal()}>
                                        {discountSchemeName}
                                    </Button>
                                </Col>
                            </Row>
                            <SalesAccountItem title={'Turnover Band:'} value={salesAccount.turnoverBandName} {...props} />
                            <SalesAccountItem title={'Eligible For Good Credit:'} value={salesAccount.eligibleForGoodCreditDiscount.toString()} {...props} />
                            <SalesAccountItem title={'Account Closed:'} value={salesAccount.closedOn} {...props} />
                            <br />
                        </Col>
                    </Row >
                    <Controls />
                </Grid>
                {/* <SalesAccountEditModal visible={salesAccountEdit.visible} {...props} /> */}
                <DiscountSchemeEditModal discountSchemeUri={salesAccount.discountSchemeUri} />
            </div>
        );
    }
}

export default SalesAccount;