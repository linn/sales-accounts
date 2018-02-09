import React, { Component } from 'react';
import { Modal, Button } from 'react-bootstrap';

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
                    <Button bsStyle="danger" onClick={() => closeAccount()}>Confirm Close</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}

export default ConfirmClose;