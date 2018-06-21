import { connect } from 'react-redux';
import ProposalItem from '../components/ProposalItem';
import { getSalesAccountId, getSalesAccountName } from '../selectors/utilities/salesAccountSelectorUtilities';
import { getSalesAccount } from '../selectors/salesAccountsSelectors';
import { getSalesAccountTurnoverBandName, getTurnoverBands, getSalesAccountDiscountSchemeName } from '../selectors/salesAccountSelectors';
import { getTurnoverBandName } from '../selectors/turnoverBandSetSelectors';
import { updateProposedTurnoverBand, excludeProposedTurnoverBand } from '../actions/turnoverBandProposal';

const mapStateToProps = ({ salesAccount, turnoverBandSets, discountSchemes, salesAccounts }, props) => ({
    proposalItem: props.proposalItem,
    currentTurnoverBandName: getTurnoverBandName(turnoverBandSets, props.proposalItem.proposedTurnoverBandUri),
    calculatedTurnoverBandName: getTurnoverBandName(turnoverBandSets, props.proposalItem.calculatedTurnoverBandUri),
    salesAccountTurnoverBandName: getSalesAccountTurnoverBandName({ 
        salesAccount: getSalesAccount(salesAccounts, props.proposalItem.salesAccountUri), 
        turnoverBandSets 
    }),
    salesAccountId: getSalesAccountId(getSalesAccount(salesAccounts, props.proposalItem.salesAccountUri)),
    salesAccountName: getSalesAccountName(getSalesAccount(salesAccounts, props.proposalItem.salesAccountUri)),
    discountSchemeName: getSalesAccountDiscountSchemeName({ 
        salesAccount: getSalesAccount(salesAccounts, props.proposalItem.salesAccountUri), 
        discountSchemes 
    }),
    displayOnly: props.proposalItem.appliedToAccount || !props.proposalItem.includeInUpdate,
    turnoverBands: getTurnoverBands({ 
        salesAccount: getSalesAccount(salesAccounts, props.proposalItem.salesAccountUri), 
        turnoverBandSets, 
        discountSchemes 
    })
});

const mapDispatchToProps = {
    updateProposedTurnoverBand,
    excludeProposedTurnoverBand
};

export default connect(mapStateToProps, mapDispatchToProps)(ProposalItem);