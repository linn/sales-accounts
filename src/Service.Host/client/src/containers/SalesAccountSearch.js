import { connect } from 'react-redux';
import SalesAccountSearch from '../components/SalesAccountSearch';
import { searchSalesAccounts, showSalesAccountSearch } from '../actions/salesAccountSearch';
import { withRouter } from 'react-router'

const mapStateToProps = ({ salesAccountSearch }) => ({
    salesAccounts: salesAccountSearch.items,
    visible: salesAccountSearch.visible,
    loading: salesAccountSearch.loading
});

const mapDispatchToProps = {
    showSalesAccountSearch,
    searchSalesAccounts
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(SalesAccountSearch));