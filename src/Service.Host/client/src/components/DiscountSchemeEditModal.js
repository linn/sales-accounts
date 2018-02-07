import React, { Component } from 'react'
import { Modal, ListGroup, ListGroupItem, Button } from 'react-bootstrap';
import { hideEditModal } from '../actions/salesAccounts';

export class DiscountSchemeEditModal extends Component {
    render() {

        const {visible, hideEditModal, discountSchemes, discountSchemeUri} = this.props;

        return (
            <Modal show={visible} onHide={() => hideEditModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ListGroup>
                        {discountSchemes.map((ds, i) => (
                            <ListGroupItem 
                                bsStyle={ds.links.find(l => l.rel === 'self').href === discountSchemeUri ? 'success' : null} 
                                key={i} 
                                onClick={() => this.handleClick(ds, discountSchemeUri)}>
                                {ds.name}
                            </ListGroupItem>
                        ))}
                    </ListGroup>
                </Modal.Body>

                <Modal.Footer>
                    <Button onClick={() => this.handleClose()}>Close</Button>
                </Modal.Footer>
            </Modal>
        )
    }

    handleClose() {
        this.props.hideEditModal();
    }

    handleClick(ds, discountSchemeUri) {
        const schemeUri = ds.links.find(l => l.rel === 'self').href;
        if (schemeUri !== discountSchemeUri){
            this.props.setDiscountScheme(schemeUri);
        }  
        this.props.hideEditModal();
    }
}

export default DiscountSchemeEditModal;
