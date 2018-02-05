import { connect } from 'react-redux';
import SalesAccount from '../components/SalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchSalesAccount, hideEditModal, showEditModal } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets } from '../actions/turnoverBandSets';
import { getSalesAccount, getSalesAccountsLoading, getDiscountSchemeName, getTurnoverBandName } from '../selectors/salesAccountsSelectors';


const mapStateToProps = ({ salesAccounts, discountSchemes, turnoverBandSets, salesAccountEditModal }, { match }) => ({
    salesAccountId: match.params.salesAccountId,
    salesAccount: getSalesAccount(match.params.salesAccountId, salesAccounts),
    discountSchemeName: getDiscountSchemeName(getSalesAccount(match.params.salesAccountId, salesAccounts), discountSchemes),
    turnoverBandName: getTurnoverBandName(getSalesAccount(match.params.salesAccountId, salesAccounts), turnoverBandSets),
    loading: getSalesAccountsLoading(salesAccounts),
    salesAccountEditModal: salesAccountEditModal
});

const initialise = ({ salesAccountId, salesAccount, loading }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountId));
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
    // dispatch(fetchTurnoverBand(salesAccount));
};

const mapDispatchToProps = {
    initialise,
    hideEditModal,
    showEditModal
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SalesAccount));