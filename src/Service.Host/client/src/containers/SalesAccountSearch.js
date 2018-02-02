import { connect } from 'react-redux';
import SalesAccountSearch from '../components/SalesAccountSearch';
import { searchSalesAccounts, clearSalesAccountSearch } from '../actions/salesAccountSearch';
import { withRouter } from 'react-router'

const mapStateToProps = ({ salesAccountSearch }) => ({
    salesAccounts: salesAccountSearch.items,
    visible: salesAccountSearch.visible,
    loading: salesAccountSearch.loading
});

const mapDispatchToProps = {
    clearSalesAccountSearch,
    searchSalesAccounts
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(SalesAccountSearch));