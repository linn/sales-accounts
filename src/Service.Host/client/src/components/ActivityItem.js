import React, { Component } from 'react';
import { ListGroupItem } from 'react-bootstrap';
import moment from 'moment';
import { getSalesAccountActivityTurnoverBandName, getSalesAccountActivityDiscountSchemeName } from '../selectors/utilities/salesAccountSelectorUtilities';

class ActivityItem extends Component {

    formatActivity(type, value, activity, updatedByName) {
        return (
            <span>
                <strong>{type} </strong>
                {value ? <span>updated to <strong>{value} </strong></span> : <span>removed </span>}
                by <b>{updatedByName ? updatedByName : 'Unknown User'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    formatCreateActivity(type, activity, updatedByName) {
        return (
            <span>
                <strong>{type} </strong>
                <span>on <strong>{moment(activity.changedOn).format('DD MMM YYYY ')}</strong></span>
                {activity.name && <span> with name <strong>{activity.name}</strong></span>}
                {activity.closedOn && <span> with closed on date <strong>{moment(activity.closedOn).format('DD MMM YYYY ')}</strong></span>}
                <span> by <b>{updatedByName ? updatedByName : 'Unknown User'}</b></span>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    formatAddressActivity(type, activity, updatedByName) {
        return (
            <span>
                <strong>{type} </strong>
                <span>updated by </span>
                <b>{updatedByName ? updatedByName : 'Unknown User'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    formatApplyTurnoverBanProposalActivity(type, value, activity, updatedByName) {
        return (
            <span>
                <strong>{type} </strong>
                <span>with turnover band <strong>{value} </strong></span>
                by <b>{updatedByName ? updatedByName : 'Unknown User'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    selectActivity(activity, discountSchemeName, turnoverBandName, updatedByName) {
        switch (activity.activityType) {
            case 'SalesAccountCloseActivity':
                return (
                    this.formatActivity('Account Closed On', moment(activity.closedOn).format('DD MMM YYYY '), activity, updatedByName)
                );

            case 'SalesAccountCreateActivity':
                return (
                    this.formatCreateActivity('Account Created ', activity, updatedByName)
                );

            case 'SalesAccountGrowthPartnerActivity':
                const growthPartner = activity.growthPartner ? 'Yes' : 'No';
                return (
                    this.formatActivity('Growth Partner', growthPartner, activity, updatedByName)
                );
            
            case 'SalesAccountUpdateAddressActivity':                
                return (
                    this.formatAddressActivity('Address', activity)
                );

            case 'SalesAccountUpdateDiscountSchemeUriActivity':
                return (
                    this.formatActivity('Discount Scheme', discountSchemeName, activity, updatedByName)
                );

            case 'SalesAccountUpdateGoodCreditActivity':
                const eligibleForGoodCreditDiscount = activity.eligibleForGoodCreditDiscount ? 'Yes' : 'No';
                return (
                    this.formatActivity('Eligible For Good Credit', eligibleForGoodCreditDiscount, activity, updatedByName)
                );

            case 'SalesAccountUpdateNameActivity':
                return (
                    this.formatActivity('Account Name', activity.name, activity, updatedByName)
                );

            case 'SalesAccountUpdateRebateActivity':
                const eligibleForRebate = activity.eligibleForRebate ? 'Yes' : 'No';
                return (
                    this.formatActivity('Eligible For Rebate', eligibleForRebate, activity, updatedByName)
                );

            case 'SalesAccountUpdateTurnoverBandUriActivity':
                return (
                    this.formatActivity('Turnover Band', turnoverBandName, activity, updatedByName)
                );

            case 'SalesAccountApplyTurnoverBandProposalActivity':
                return (
                    this.formatApplyTurnoverBanProposalActivity('Turnover Band Proposal Applied', turnoverBandName, activity, updatedByName)
                );

            default:
                return null;
        }
    }

    render() {
        const { activity, discountSchemeName, turnoverBandName, updatedByName } = this.props;
        return (
            <ListGroupItem>{this.selectActivity(activity, discountSchemeName, turnoverBandName, updatedByName)}</ListGroupItem>
        );
    }
}

export default ActivityItem;