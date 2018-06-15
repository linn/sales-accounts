import React, { Component } from 'react';
import { Button, Row, Col, Well, Glyphicon } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import  ConfirmClose from './ConfirmClose';
import { combineReducers } from 'redux';

class Controls extends Component {

    state = {
        showConfirmClose: false
    }
    render() {
        const { closedOn, dirty, saving, salesAccount, saveAccountUpdate, salesAccountUri } = this.props;

        return (
            <div>
                <Row style={{ marginTop: '20px' }}>
                    <Col xs={12}>
                        <Well>
                            <LinkContainer to='/sales/accounts'>
                                <Button bsStyle="link">Back</Button>
                            </LinkContainer>
                          
                            <Button 
                                disabled={!dirty} 
                                style={{marginLeft: '20px'}} bsStyle="primary" className=" muted pull-right" 
                                onClick={() => saveAccountUpdate(salesAccount)}>
                                {saving 
                                    ? <span>Saving <Glyphicon glyph="glyphicon glyphicon-repeat gly-spin" style={{marginLeft: '4px'}} /> </span> 
                                    : <span>Save</span>
                                }
                            </Button>
                            {/* uncomment this when we wish to turn on ability to close account from the app
                            {!closedOn &&  
                                <Button bsStyle="danger" className="muted pull-right" onClick={() => this.handleShowConfimClose()}>Close Account</Button>
                            } */}
                        </Well>
                    </Col>
                </Row>
                <ConfirmClose id={salesAccount.id} closeAccount={() => this.handleCloseAccount()} cancelClose={() => this.cancelClose()} visible={this.state.showConfirmClose}/>
            </div>
        );
    }

    handleShowConfimClose() {
        this.setState({ showConfirmClose: true });
    }

    cancelClose() {
        this.setState({ showConfirmClose: false });
    }

    handleCloseAccount() {
        this.setState({showConfirmClose: false});
        this.props.closeAccount(this.props.salesAccountUri);
    }
}

export default Controls;
