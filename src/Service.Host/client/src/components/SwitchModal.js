import React, { Component } from 'react'
import { Modal, ListGroup, ListGroupItem, Button, ButtonGroup, ToggleButtonGroup, ToggleButton } from 'react-bootstrap';
import { combineReducers } from 'redux';

export class SwitchModal extends Component {

    constructor(props, context) {
        super(props, context);

        this.handleChange = this.handleChange.bind(this);

    }
    render() {
        const { visible, title, hideModal, value1, value2, current } = this.props;
   
        return (
            <Modal bsSize={'sm'} show={visible} onHide={() => hideModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                <Modal.Body style={{ textAlign: 'center' }}>
                    <ToggleButtonGroup type="radio" name="options" value={current} onChange={this.handleChange}>
                        <ToggleButton value={true} onClick={() => this.handleClick(true)} >{value1}</ToggleButton>
                        <ToggleButton value={false} onClick={() => this.handleClick(false)} >{value2}</ToggleButton>
                    </ToggleButtonGroup>
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={() => hideModal()}>Close</Button>
                </Modal.Footer>
            </Modal>
        )
    }

    handleChange() {
        //ToggleButtonGroup onChange doesn't work when using radio and boolena values, but need to present to avoid a warning
    }

    handleClick(value) {
        this.props.setValue(value);
        this.props.hideModal();
    }
}

export default SwitchModal;