import React, { Component } from 'react';
import { Button, Row, Col, Well } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import  ConfirmClose from './ConfirmClose';

class Controls extends Component {

    state = {
        showConfirmClose: false
    }
    render() {
        const { closedOn, salesAccountId, closeAccount } = this.props;

        return (
            <div>
                <Row style={{ marginTop: '20px' }}>
                    <Col xs={12}>
                        <Well>
                            <LinkContainer to='/sales/accounts'>
                                <Button bsStyle="link">Back</Button>
                            </LinkContainer>
                            <Button  style={{marginLeft: '20px'}} bsStyle="primary" className=" muted pull-right" onClick={() => this.handleSave()}>Save</Button>
                            {!closedOn &&  
                                <Button bsStyle="danger" className=" muted pull-right" onClick={() => this.handleShowConfimClose()}>Close Account</Button>
                             }
                        </Well>
                    </Col>
                </Row>
                <ConfirmClose id={salesAccountId} closeAccount={closeAccount} cancelClose={() => this.cancelClose()} visible={this.state.showConfirmClose}/>
            </div>
        );
    }

    handleShowConfimClose() {
        this.setState({ showConfirmClose: true });
    }

    cancelClose() {
        this.setState({ showConfirmClose: false });
    }
}

export default Controls;
