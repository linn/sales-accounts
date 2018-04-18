import React, { Component } from 'react';
import { ListGroupItem } from 'react-bootstrap';
import moment from 'moment';
import { getTurnoverBandName, getDiscountSchemeName, getTurnoverBandSet } from '../selectors/salesAccountSelectors';

class ActivityItem extends Component {

    formatActivity(type, value, changedOn) {
        return (
            <span>
                <strong>{type} </strong>
                {value ? <span>updated <strong>{value}</strong></span> : <span>removed</span>}
                <span className="small pull-right text-muted">{moment(changedOn).startOf('hour').fromNow()}</span>
            </span>
        );
    }

    formatCreateActivity(type, createdDate, name, closedOn, changedOn) {
        return (
            <span>
                <strong>{type} </strong>
                <span>on <strong>{moment(createdDate).format('DD MMM YYYY ')}</strong></span>
                {name && <span> with name <strong>{name}</strong></span>}
                {closedOn && <span> with closed on date <strong>{moment(closedOn).format('DD MMM YYYY ')}</strong></span>}
                <span className="small pull-right text-muted">{moment(changedOn).startOf('hour').fromNow()}</span>
            </span>
        );
    }

    selectActivity(activity, discountSchemes, turnoverBandSets) {
        switch (activity.activityType) {
            case 'SalesAccountCloseActivity':
                return (
                    this.formatActivity('Account Closed On', moment(activity.closedOn).format('DD MMM YYYY '), activity.changedOn)
                );

            case 'SalesAccountCreateActivity':
                return (
                    this.formatCreateActivity('Account Created ', activity.changedOn, activity.name, activity.closedOn, activity.changedOn)
                );

            case 'SalesAccountGrowthPartnerActivity':
                const growthPartner = activity.growthPartner ? 'Yes' : 'No';
                return (
                    this.formatActivity('Growth Partner', growthPartner, activity.changedOn)
                );
            
            case 'SalesAccountUpdateAddressActivity':                
                return (
                    this.formatActivity('Address', ' ', activity.changedOn)
                );

            case 'SalesAccountUpdateDiscountSchemeUriActivity':
                const discountScheme = getDiscountSchemeName(activity, discountSchemes);
                return (
                    this.formatActivity('Discount Scheme', discountScheme, activity.changedOn)
                );

            case 'SalesAccountUpdateGoodCreditActivity':
                const eligibleForGoodCreditDiscount = activity.eligibleForGoodCreditDiscount ? 'Yes' : 'No';
                return (
                    this.formatActivity('Eligible For Good Credit', eligibleForGoodCreditDiscount, activity.changedOn)
                );

            case 'SalesAccountUpdateNameActivity':
                return (
                    this.formatActivity('Account Name', activity.name, activity.changedOn)
                );

            case 'SalesAccountUpdateRebateActivity':
                const eligibleForRebate = activity.eligibleForRebate ? 'Yes' : 'No';
                return (
                    this.formatActivity('Eligible For Rebate', eligibleForRebate, activity.changedOn)
                );

            case 'SalesAccountUpdateTurnoverBandUriActivity':
                const turnoverBand = getTurnoverBandName(activity, turnoverBandSets);                
                return (
                    this.formatActivity('Turnover Band', turnoverBand, activity.changedOn)
                );

            default:
                return null;
        }
    }

    render() {
        const { activity, discountSchemes, turnoverBandSets } = this.props;
        return (
            <ListGroupItem>{this.selectActivity(activity, discountSchemes, turnoverBandSets)}</ListGroupItem>
        );
    }
}

export default ActivityItem;