import React, { Component } from 'react'
import { Modal, ListGroup, ListGroupItem, Button } from 'react-bootstrap';

export class ListSelectItemModal extends Component {
    render() {

        const { visible, title, hideModal, items = [], currentItemUri } = this.props;

        return (
            <Modal show={visible} onHide={() => hideModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ListGroup>
                        {items.map((item, i) => (
                            <ListGroupItem 
                                bsStyle={item.links.find(l => l.rel === 'self').href === currentItemUri ? 'success' : null} 
                                key={i} 
                                onClick={() => this.handleClick(item, currentItemUri)}>
                                {item.name}
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
        this.props.hideModal();
    }

    handleClick(item, currentItemUri) {
        const { hideModal, setItem } = this.props;
        const itemUri = item.links.find(l => l.rel === 'self').href;
        if (itemUri !== currentItemUri){
            setItem(itemUri);
        }  
        hideModal();
    }
}

export default ListSelectItemModal;