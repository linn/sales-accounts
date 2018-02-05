import React, { Component } from 'react'
import { Modal } from 'react-bootstrap';

export class SalesAccountEditModal extends Component {
    render() {
        return (
            <Modal show={this.props.visible} bsSize="large" onHide={() => this.props.hideEditModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit</Modal.Title>
                </Modal.Header>
                <Modal.Body>

                </Modal.Body>
   
                <Modal.Footer>

                </Modal.Footer>
            </Modal>
        )
    }
}

export default SalesAccountEditModal;
