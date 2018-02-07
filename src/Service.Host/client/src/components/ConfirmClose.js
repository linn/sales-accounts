import React, { Component } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';

class ConfirmClose extends Component {

    render() {
        const { id, closeAccount, cancelClose, visible } = this.props;

        return (
            <Modal show={visible}>
               <Modal.Body>
                    <h3 style={{textAlign: 'center'}}>Are you sure you want to close this sales account?</h3>
                </Modal.Body>  
               <Modal.Footer>
                    <Button bsStyle="link" onClick={() => cancelClose()}>Cancel</Button>
                    <LinkContainer to={`/sales/accounts`}>
                        <Button bsStyle="danger" onClick={() => closeAccount(id)}>Confirm Close</Button>
                   </LinkContainer>
                </Modal.Footer>
            </Modal>
        );
    }
}

export default ConfirmClose;