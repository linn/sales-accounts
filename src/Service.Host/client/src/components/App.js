import React, { Component } from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import SalesAccountSearch from '../containers/SalesAccountSearch';

class App extends Component {
    render() {
        return (
            <div>
                <div>
                    <h1>Here we are</h1>
                    <SalesAccountSearch />
                </div>
                <Grid fluid={false}>
                    <Row>
                        <Col xs={12}>
                        </Col>
                    </Row >
                </Grid>
            </div>
        );
    }
}

export default App;