import React, { Component } from 'react'
import { Modal, Button, ToggleButtonGroup, ToggleButton } from 'react-bootstrap';
import { combineReducers } from 'redux';

export class BooleanSwitchModal extends Component {

    render() {
        const { visible, title, hideModal, trueText, falseText, current } = this.props;

        return (
            <Modal bsSize={'sm'} show={visible} onHide={() => hideModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                <Modal.Body style={{ textAlign: 'center' }}>
                    <ToggleButtonGroup type="radio" name="options" value={current} onChange={this.handleChange}>
                        <ToggleButton value={true} onClick={() => this.handleClick(true)} >{trueText}</ToggleButton>
                        <ToggleButton value={false} onClick={() => this.handleClick(false)} >{falseText}</ToggleButton>
                    </ToggleButtonGroup>
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={() => hideModal()}>Close</Button>
                </Modal.Footer>
            </Modal>
        );
    }

    handleChange() {
        // ToggleButtonGroup onChange doesn't work when using radio and boolean values.
        // onChange needs to be present to avoid a console warning
    }

    handleClick(value) {
        this.props.setValue(value);
        this.props.hideModal();
    }
}

export default BooleanSwitchModal;