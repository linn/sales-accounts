import React, { Component } from 'react'
import { Modal, ListGroup, ListGroupItem } from 'react-bootstrap';
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
                                onClick={() => this.handleClick(ds)}>
                                {ds ? ds.name : null}
                            </ListGroupItem>
                        ))}
                    </ListGroup>
                </Modal.Body>

                <Modal.Footer>

                </Modal.Footer>
            </Modal>
        )
    }

    handleClick(ds) {
        this.props.setDiscountScheme(ds.links.find(l => l.rel === 'self').href);
        this.props.hideEditModal();
    }
}

export default DiscountSchemeEditModal;
