import React, { Component } from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import SalesAccountSearch from '../containers/SalesAccountSearch';

class App extends Component {
    render() {
        return (
            <div>
                <Grid fluid={false}>
                    <Row>
                        <Col xs={8}>
                            <div>
                                <SalesAccountSearch />
                            </div>
                        </Col>
                    </Row >
                </Grid>
            </div>
        );
    }
}

export default App;