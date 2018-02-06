import { connect } from 'react-redux';
import SalesAccount from '../components/SalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchSalesAccount, hideEditModal, showEditModal } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';
import { getSalesAccount, getSalesAccountsLoading, getDiscountSchemeName, getTurnoverBandName } from '../selectors/salesAccountsSelectors';


const mapStateToProps = ({ salesAccount, discountSchemes, turnoverBandSets, salesAccountEditModal }, { match }) => ({
    salesAccountUri: match.url,
    salesAccount: getSalesAccount(salesAccount),
    discountSchemeName: getDiscountSchemeName(getSalesAccount(salesAccount), discountSchemes),
    turnoverBandName: getTurnoverBandName(getSalesAccount(salesAccount), turnoverBandSets),
    loading: salesAccount.loading,
    salesAccountEditModal: salesAccountEditModal
});

const initialise = ({ salesAccountUri, salesAccount, loading }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountUri));
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
    dispatch(fetchTurnoverBand(salesAccount));
};

const mapDispatchToProps = {
    initialise,
    hideEditModal,
    showEditModal
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SalesAccount));