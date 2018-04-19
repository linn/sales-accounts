import { connect } from 'react-redux';
import SalesAccount from '../components/SalesAccount';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';
import { 
    fetchSalesAccount, saveAccountUpdate, hideEditModal,
    closeAccount, showConfirmCloseModal, hideConfirmCloseModal,
    editDiscountScheme, setDiscountScheme,
    editTurnoverBand, setTurnoverBand, 
    editEligibleForGoodCreditDiscount, setEligibleForGoodCreditDiscount,
    editGrowthPartner, setGrowthPartner,
    editEligibleForRebate, setEligibleForRebate,
    fetchCountry, fetchActivities
} from '../actions/salesAccounts';
import { getSalesAccount, getDiscountSchemeName, getTurnoverBandName, getTurnoverBands, getDiscountSchemes, getDiscountSchemeClosedOn, getActivities } from '../selectors/salesAccountSelectors';

const mapStateToProps = ({ salesAccount, discountSchemes, turnoverBandSets }, { match }) => ({
    salesAccountUri: match.url,
    salesAccount: getSalesAccount(salesAccount),
    discountSchemeName: getDiscountSchemeName(getSalesAccount(salesAccount), discountSchemes),
    discountSchemeStatus: getDiscountSchemeClosedOn(getSalesAccount(salesAccount), discountSchemes),
    turnoverBandName: getTurnoverBandName(getSalesAccount(salesAccount),turnoverBandSets),
    discountSchemes: getDiscountSchemes(discountSchemes),
    turnoverBands: getTurnoverBands(salesAccount, turnoverBandSets, discountSchemes),
    editGoodCreditVisible: salesAccount.editGoodCreditVisible,
    editDiscountSchemeVisible: salesAccount.editDiscountSchemeVisible,
    editTurnoverBandVisible: salesAccount.editTurnoverBandVisible,
    editGrowthPartnerVisible: salesAccount.editGrowthPartnerVisible,
    editEligibleForRebateVisible: salesAccount.editEligibleForRebateVisible,
    loading: salesAccount.loading || !discountSchemes || !turnoverBandSets,
    dirty: salesAccount.dirty,
    saving: salesAccount.saving,
    activities: getActivities(salesAccount),
    turnoverBandSets: turnoverBandSets
});

const initialise = ({ salesAccountUri, salesAccount }) => dispatch => {
    dispatch(fetchSalesAccount(salesAccountUri));
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
    dispatch(fetchActivities(salesAccountUri));
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