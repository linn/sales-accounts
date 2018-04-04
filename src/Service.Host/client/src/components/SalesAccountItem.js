import React, { Component } from 'react'
import { Grid, Row, Col, Button, Label } from 'react-bootstrap';

const styles = {
    title: {
        textAlign: 'right',
        marginBottom: '6px'
    },
   button: {
        padding: '0', 
        outline: 0 
   }
}

export class SalesAccountItem extends Component {
    render() {
        const {title, value, handleClick, displayOnly, label} = this.props;
        return (
            <Row>
                <Col sm={4} style={styles.title}>
                    <b>{title}</b>
                </Col>
                <Col sm={4}>
                    {displayOnly 
                        ? value 
                        : <Button bsStyle="link" style={styles.button} onClick={() => handleClick()}>{value}</Button>
                    }
                    {label && <Label bsStyle="danger" style={{ marginLeft: 10 }}>{label}</Label>}
                </Col>
            </Row>
        )
    }
}

export default SalesAccountItem;
