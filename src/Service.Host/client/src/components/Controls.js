import React, { Component } from 'react';
import { Button, Row, Col, Well } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { LinkContainer } from 'react-router-bootstrap';

class Controls extends Component {

    render() {
        const { closedOn } = this.props;

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
            </div>
        );
    }
}

export default Controls;
