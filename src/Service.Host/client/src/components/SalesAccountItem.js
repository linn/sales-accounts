import React, { Component } from 'react'
import { Grid, Row, Col, Button } from 'react-bootstrap';

const styles = {
    item: {
        textAlign: 'right',
        marginBottom: '6px'
    }
}

export class SalesAccountItem extends Component {
    render() {
        const {title, value, children} = this.props;
        return (
            <Row>
                <Col sm={4} style={styles.item}>
                    <b>{title}</b>
                </Col>
                <Col sm={2}>
                    <Button bsStyle="link" style={{ padding: '0' }}>
                        {value}
                    </Button>  
                </Col>
            </Row>
        )
    }
}

export default SalesAccountItem;
