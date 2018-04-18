import React, { Component } from 'react';
import { Button, Alert, ListGroup } from 'react-bootstrap';
import moment from 'moment';
import ActivityItem from './ActivityItem';

class Activities extends Component {    

    constructor(props) {
        super(props);
        this.state = {
            viewActivities: false
        }
    }
    
    compare(a,b) {
        return moment.utc(b.changedOn).diff(moment.utc(a.changedOn));
    }

    handleViewActivitiesClick() {
        this.setState({viewActivities: !this.state.viewActivities});
    }

    render() {
        const { activities, discountSchemes, turnoverBandSets } = this.props;        

        return activities && activities.length > 0
        ? (
            <div>                
                <h4 className="pull-left">Activity History</h4>
                <Button bsStyle="default" className="muted" style={{ marginLeft: '10px', marginBottom: '10px' }} onClick={() => this.handleViewActivitiesClick()}>
                    {this.state.viewActivities ? 'Hide' : 'View'}
                </Button>
                {this.state.viewActivities
                    ? (
                        <ListGroup>
                            {activities
                                .sort((a,b) => this.compare(a,b))
                                .map((activity, i) => (
                                    <ActivityItem activity={activity} discountSchemes={discountSchemes} turnoverBandSets={turnoverBandSets} key={i} />
                                ))
                            }
                        </ListGroup>
                    )
                    : (<span/>)
                }
            </div>
        )
        : (
            <Alert bsStyle="warning">This sales account has no activities</Alert>
        );
    }
}

export default Activities;