import { connect } from 'react-redux';
import SalesAccount from '../components/SalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { 
    fetchSalesAccount, saveAccountUpdate, hideEditModal,
    closeAccount, showConfirmCloseModal, hideConfirmCloseModal,
    editDiscountScheme, setDiscountScheme,
    editTurnoverBand, setTurnoverBand, 
    editEligibleForGoodCreditDiscount, setEligibleForGoodCreditDiscount,
    editGrowthPartner, setGrowthPartner,
    editEligibleForRebate, setEligibleForRebate
 } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';
import { getSalesAccount, getDiscountSchemeName, getTurnoverBands, getTurnoverBandSet, getTurnoverBandSetUri, getDiscountScheme, getDiscountSchemeUri, getTurnoverBandName} from '../selectors/salesAccountsSelectors';

const mapStateToProps = ({ salesAccount, discountSchemes, turnoverBandSets }, { match }) => ({
    salesAccountUri: match.url,
    salesAccount: getSalesAccount(salesAccount),
    discountSchemeName: getDiscountSchemeName(getSalesAccount(salesAccount), discountSchemes),
    turnoverBandName: getTurnoverBandName(getSalesAccount(salesAccount),turnoverBandSets),
    discountSchemes: discountSchemes,
    turnoverBands: getTurnoverBands(getTurnoverBandSet(turnoverBandSets, getTurnoverBandSetUri(getDiscountScheme(discountSchemes, getDiscountSchemeUri(salesAccount))))),
    editGoodCreditVisible: salesAccount.editGoodCreditVisible,
    editDiscountSchemeVisible: salesAccount.editDiscountSchemeVisible,
    editTurnoverBandVisible: salesAccount.editTurnoverBandVisible,
    editGrowthPartnerVisible: salesAccount.editGrowthPartnerVisible,
    editEligibleForRebateVisible: salesAccount.editEligibleForRebateVisible,
    loading: salesAccount.loading || !discountSchemes.length || !turnoverBandSets.length,
    dirty: salesAccount.dirty,
    saving: salesAccount.saving
});

const initialise = ({ salesAccountUri, salesAccount, loading }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountUri));
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
};

const mapDispatchToProps = {
    initialise,
    hideEditModal,
    closeAccount,
    saveAccountUpdate,
    editDiscountScheme,
    setDiscountScheme,
    editTurnoverBand,
    setTurnoverBand,
    editEligibleForGoodCreditDiscount,
    setEligibleForGoodCreditDiscount,
    editGrowthPartner,
    setGrowthPartner,
    editEligibleForRebate,
    setEligibleForRebate,
    showConfirmCloseModal,
    hideConfirmCloseModal
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SalesAccount));