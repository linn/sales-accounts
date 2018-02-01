import { connect } from 'react-redux';
import SalesAccountSearch from '../components/SalesAccountSearch';
import { searchSalesAccounts, hideSalesAccountSearch } from '../actions/salesAccountSearch';
import { fetchSalesAccount } from '../actions/salesAccounts';

const mapStateToProps = ({ salesAccountSearch }) => ({
    salesAccounts: salesAccountSearch.items,
    visible: salesAccountSearch.visible,
    loading: salesAccountSearch.loading
});

const mapDispatchToProps = {
    hideSalesAccountSearch,
    searchSalesAccounts,
    fetchSalesAccount
};

export default connect(mapStateToProps, mapDispatchToProps)(SalesAccountSearch);