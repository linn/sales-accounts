import { connect } from 'react-redux';
import TurnoverBandProposal from '../components/TurnoverBandProposal';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchAllSalesAccounts } from '../actions/salesAccounts';
import { fetchTurnoverBandProposal } from '../actions/turnoverBandProposal';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets } from '../actions/turnoverBandSets';
import { getLoading, getProposedTurnoverBands, getFinancialYear } from '../selectors/turnoverBandProposalSelectors';

const mapStateToProps = ({ turnoverBandProposal, salesAccounts, discountSchemes, turnoverBandSets }) => ({
    loading: getLoading(turnoverBandProposal),
    proposedTurnoverBands: getProposedTurnoverBands(turnoverBandProposal),
    financialYear: getFinancialYear(turnoverBandProposal),
    salesAccounts,
    discountSchemes,
    turnoverBandSets
});

const initialise = () => dispatch => {
    dispatch(fetchTurnoverBandProposal());
    dispatch(fetchAllSalesAccounts());
    dispatch(fetchDiscountSchemes());
    dispatch(fetchTurnoverBandSets());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TurnoverBandProposal));