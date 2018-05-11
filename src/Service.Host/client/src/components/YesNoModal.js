import React, { Component } from 'react'
import { Modal, Button } from 'react-bootstrap';

export class YesNoModal extends Component {
    render() {
        const { visible, title, hideModal, yesButtonText, noButtonText, text, instructionText} = this.props;

        return (
            <Modal bsSize={'sm'} show={visible} onHide={() => hideModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {text} 
                    <br /><br />
                    {instructionText}
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={() => this.handleNoClick()}>{noButtonText}</Button>
                    <Button onClick={() => this.handleYesClick()}>{yesButtonText}</Button>
                </Modal.Footer>
            </Modal>
        );
    }

    handleYesClick() {
        if (this.props.performYesAction) {
            this.props.performYesAction();
        }

        this.props.hideModal();
    }

    handleNoClick() {
        if (this.props.performNoAction) {
            this.props.performNoAction();
        }

        this.props.hideModal();
    }
}

export default YesNoModal;