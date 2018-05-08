import { connect } from 'react-redux';
import TurnoverBandProposal from '../components/TurnoverBandProposal';
import initialiseOnMount from './common/initialiseOnMount';
import { fetchTurnoverBandProposal } from '../actions/turnoverBandProposal';
import { getLoading, getProposedTurnoverBands, getFinancialYear } from '../selectors/turnoverBandProposalSelectors';

const mapStateToProps = ({ turnoverBandProposal }) => ({
    loading: getLoading(turnoverBandProposal),
    proposedTurnoverBands: getProposedTurnoverBands(turnoverBandProposal),
    financialYear: getFinancialYear(turnoverBandProposal)
});

const initialise = () => dispatch => {
    dispatch(fetchTurnoverBandProposal());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TurnoverBandProposal));