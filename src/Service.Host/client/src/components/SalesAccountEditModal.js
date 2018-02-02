import React, { Component } from 'react'
import { Modal } from 'react-bootstrap';

export class SalesAccountEditModal extends Component {
    render() {
        return (
            <Modal show={update.visible} bsSize="large" onHide={() => hideUpdateModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{Edit}</Modal.Title>
                </Modal.Header>
                <Modal.Body>

                </Modal.Body>
                {children}
                <Modal.Footer>

                </Modal.Footer>
            </Modal>
        )
    }
}

export default SalesAccountEditModal;
