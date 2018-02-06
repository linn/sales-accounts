import { connect } from 'react-redux';
import SalesAccount from '../components/SalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchSalesAccount, hideEditModal, showEditModal, editDiscountScheme } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';
import { getSalesAccount, getDiscountSchemeName } from '../selectors/salesAccountsSelectors';


const mapStateToProps = ({ salesAccount, discountSchemes, turnoverBandSets, salesAccountEdit }, { match }) => ({
    salesAccountUri: match.url,
    salesAccount: getSalesAccount(salesAccount),
    discountSchemeName: getDiscountSchemeName(getSalesAccount(salesAccount), discountSchemes),
    loading: salesAccount.loading,
    salesAccountEdit: salesAccountEdit
});

const initialise = ({ salesAccountUri, salesAccount, loading }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountUri));
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
};

const mapDispatchToProps = {
    initialise,
    hideEditModal,
    showEditModal,
    editDiscountScheme
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SalesAccount));