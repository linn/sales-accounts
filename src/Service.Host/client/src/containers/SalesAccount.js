import { connect } from 'react-redux';
import SalesAccount from '../components/SalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchSalesAccount, hideEditModal, setDiscountScheme,
    editDiscountScheme, showDiscountSchemeEditModal, showTurnoverBandEditModal, editGoodCredit, setTurnoverBand } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';
import { getSalesAccount, getDiscountSchemeName, getTurnoverBands, getTurnoverBandSet, getTurnoverBandSetUri, getDiscountScheme, getDiscountSchemeUri, getTurnoverBandName2} from '../selectors/salesAccountsSelectors';


const mapStateToProps = ({ salesAccount, discountSchemes, turnoverBandSets }, { match }) => ({
    salesAccountUri: match.url,
    salesAccount: getSalesAccount(salesAccount),
    
    discountSchemeName: getDiscountSchemeName(getSalesAccount(salesAccount), discountSchemes),
    turnoverBandName: getTurnoverBandName2(getSalesAccount(salesAccount),turnoverBandSets),

    discountSchemes: discountSchemes,
    turnoverBands: getTurnoverBands(getTurnoverBandSet(turnoverBandSets, getTurnoverBandSetUri(getDiscountScheme(discountSchemes, getDiscountSchemeUri(salesAccount))))),
    loading: salesAccount.loading,
    editGoodCreditVisible: salesAccount.editGoodCreditVisible,
    editDiscountSchemeVisible: salesAccount.editDiscountSchemeVisible,
    editTurnoverBandVisible: salesAccount.editTurnoverBandVisible
});

const initialise = ({ salesAccountUri, salesAccount, loading }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountUri));
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
};

const mapDispatchToProps = {
    initialise,
    hideEditModal,
    editDiscountScheme,
    setDiscountScheme,
    showTurnoverBandEditModal,
    setTurnoverBand,
    showDiscountSchemeEditModal,
    editGoodCredit
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SalesAccount));