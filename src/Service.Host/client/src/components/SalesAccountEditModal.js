import React, { Component } from 'react'
import { Modal, ListGroup } from 'react-bootstrap';

export class SalesAccountEditModal extends Component {
    render() {

        const {} = this.props;

        return (
            <Modal show={this.props.visible} onHide={() => this.props.hideEditModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {/* <ListGroup>
                        {items.map((item, i) => (
                            <ListGroupItem key={i} onClick={() => this.handleProductClick(item)}>
                                {item ? item.name : null}
                            </ListGroupItem>
                        ))}
                    </ListGroup> */}
                </Modal.Body>

                <Modal.Footer>

                </Modal.Footer>
            </Modal>
        )
    }
}

export default SalesAccountEditModal;
