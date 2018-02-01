import { connect } from 'react-redux';
import ViewSalesAccount from '../components/ViewSalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchSalesAccount } from '../actions/salesAccounts';
import { getSalesAccount, getSalesAccountsLoading } from '../selectors/salesAccountsSelectors';

const mapStateToProps = ({ salesAccounts }, { match }) => ({
    salesAccountId: match.params.salesAccountId,
    salesAccount: getSalesAccount(match.params.salesAccountId, salesAccounts),
    loading: getSalesAccountsLoading(salesAccounts)
});

const initialise = ({ salesAccountId, salesAccount, loading }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountId));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ViewSalesAccount));