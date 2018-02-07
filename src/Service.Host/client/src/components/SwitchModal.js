import React, { Component } from 'react'
import { Modal, ListGroup, ListGroupItem, Button,ToggleButtonGroup, ToggleButton } from 'react-bootstrap';

export class SwitchModal extends Component {
    render() {

        const { visible, title, hideModal, items, currentItemUri } = this.props;

        return (
            <Modal show={visible} onHide={() => hideModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ListGroup>
                        <ToggleButtonGroup type="radio" name="options">
                            <ToggleButton value={2}>Yes</ToggleButton>
                            <ToggleButton value={3}>No</ToggleButton>
                        </ToggleButtonGroup>
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

    // handleClick(item, currentItemUri) {
    //     const { hideEditModal, setItem } = this.props;
    //     const itemUri = item.links.find(l => l.rel === 'self').href;
    //     if (itemUri !== currentItemUri) {
    //         setItem(itemUri);
    //     }
    //     hideEditModal();
    // }
}

export default SwitchModal;