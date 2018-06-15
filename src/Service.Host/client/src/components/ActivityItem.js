import React, { Component } from 'react';
import { ListGroupItem } from 'react-bootstrap';
import moment from 'moment';
import { getSalesAccountTurnoverBandName, getDiscountSchemeName } from '../selectors/salesAccountSelectors';

class ActivityItem extends Component {

    formatActivity(type, value, activity) {
        return (
            <span>
                <strong>{type} </strong>
                {value ? <span>updated to <strong>{value} </strong></span> : <span>removed </span>}
                by <b>{activity.updatedByName ? activity.updatedByName : 'unknown user'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    formatCreateActivity(type, activity) {
        return (
            <span>
                <strong>{type} </strong>
                <span>on <strong>{moment(activity.changedOn).format('DD MMM YYYY ')}</strong></span>
                {activity.name && <span> with name <strong>{activity.name}</strong></span>}
                {activity.closedOn && <span> with closed on date <strong>{moment(activity.closedOn).format('DD MMM YYYY ') }</strong></span>}
                by <b>{activity.updatedByName ? activity.updatedByName : 'unknown user'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    formatAddressActivity(type, activity) {
        return (
            <span>
                <strong>{type} </strong>
                <span>updated by </span>
                <b>{activity.updatedByName ? activity.updatedByName : 'unknown user'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    formatApplyTurnoverBanProposalActivity(type, value, activity) {
        return (
            <span>
                <strong>{type} </strong>
                <span>with turnover band <strong>{value} </strong></span>
                by <b>{activity.updatedByName ? activity.updatedByName : 'unknown user'}</b>
                <span className="small pull-right text-muted">{moment(activity.changedOn).fromNow()}</span>
            </span>
        );
    }

    selectActivity(activity, discountSchemes, turnoverBandSets) {
        switch (activity.activityType) {
            case 'SalesAccountCloseActivity':
                return (
                    this.formatActivity('Account Closed On', moment(activity.closedOn).format('DD MMM YYYY '), activity)
                );

            case 'SalesAccountCreateActivity':
                return (
                    this.formatCreateActivity('Account Created ', activity)
                );

            case 'SalesAccountGrowthPartnerActivity':
                const growthPartner = activity.growthPartner ? 'Yes' : 'No';
                return (
                    this.formatActivity('Growth Partner', growthPartner, activity)
                );
            
            case 'SalesAccountUpdateAddressActivity':                
                return (
                    this.formatAddressActivity('Address', activity)
                );

            case 'SalesAccountUpdateDiscountSchemeUriActivity':
                const discountScheme = getDiscountSchemeName(activity, discountSchemes);
                return (
                    this.formatActivity('Discount Scheme', discountScheme, activity)
                );

            case 'SalesAccountUpdateGoodCreditActivity':
                const eligibleForGoodCreditDiscount = activity.eligibleForGoodCreditDiscount ? 'Yes' : 'No';
                return (
                    this.formatActivity('Eligible For Good Credit', eligibleForGoodCreditDiscount, activity)
                );

            case 'SalesAccountUpdateNameActivity':
                return (
                    this.formatActivity('Account Name', activity.name, activity)
                );

            case 'SalesAccountUpdateRebateActivity':
                const eligibleForRebate = activity.eligibleForRebate ? 'Yes' : 'No';
                return (
                    this.formatActivity('Eligible For Rebate', eligibleForRebate, activity)
                );

            case 'SalesAccountUpdateTurnoverBandUriActivity':
                const turnoverBand = getSalesAccountTurnoverBandName(activity, turnoverBandSets);
                return (
                    this.formatActivity('Turnover Band', turnoverBand, activity)
                );

            case 'SalesAccountApplyTurnoverBandProposalActivity':
                const proposalTurnoverBand = getSalesAccountTurnoverBandName(activity, turnoverBandSets);
                return (
                    this.formatApplyTurnoverBanProposalActivity('Turnover Band Proposal Applied', proposalTurnoverBand, activity)
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