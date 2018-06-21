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
import { getSalesAccount, getSalesAccountDiscountSchemeName, getSalesAccountTurnoverBandName, getTurnoverBands, getDiscountSchemeClosedOn, getSalesAccountActivities, getSalesAccountLoading } from '../selectors/salesAccountSelectors';
import { getDiscountSchemes } from '../selectors/discountSchemesSelectors';
import { getEmployeesLoading } from '../selectors/utilities/employeeSelectorUtilities';

const mapStateToProps = (state, { match }) => ({
    salesAccountUri: match.url,
    salesAccount: getSalesAccount(state),
    discountSchemeName: getSalesAccountDiscountSchemeName(state),
    discountSchemeStatus: getDiscountSchemeClosedOn(state),
    turnoverBandName: getSalesAccountTurnoverBandName(state),
    discountSchemes: getDiscountSchemes(state),
    turnoverBands: getTurnoverBands(state),
    editGoodCreditVisible: state.salesAccount.editGoodCreditVisible,
    editDiscountSchemeVisible: state.salesAccount.editDiscountSchemeVisible,
    editTurnoverBandVisible: state.salesAccount.editTurnoverBandVisible,
    editGrowthPartnerVisible: state.salesAccount.editGrowthPartnerVisible,
    editEligibleForRebateVisible: state.salesAccount.editEligibleForRebateVisible,
    loading: getSalesAccountLoading(state),
    dirty: state.salesAccount.dirty,
    saving: state.salesAccount.saving,
    activities: getSalesAccountActivities(state),
    turnoverBandSets: state.turnoverBandSets
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