import React, { Component } from 'react'
import { Modal, ListGroup, ListGroupItem, Button } from 'react-bootstrap';
import { hideEditModal } from '../actions/salesAccounts';

export class TurnoverBandEditModal extends Component {
    render() {

        const {visible, hideEditModal, turnoverBandUri, turnoverBands} = this.props;

        return (
            <Modal show={visible} onHide={() => hideEditModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ListGroup>
                        {turnoverBands && turnoverBands.map((tb, i) => (
                            <ListGroupItem 
                                key={i} 
                                bsStyle={tb.links.find(l => l.rel === 'self').href === turnoverBandUri ? 'success' : null} 
                                onClick={() => this.handleClick(tb, turnoverBandUri)}>
                                {tb.name}
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

    handleClick(tb, turnoverBandUri) {
        const bandUri = tb.links.find(l => l.rel === 'self').href;
        if (bandUri !== turnoverBandUri){
            this.props.setTurnoverBand(bandUri, tb.name);
        }  
        this.props.hideEditModal();
    }
}

export default TurnoverBandEditModal;
