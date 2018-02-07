import { connect } from 'react-redux';
import TurnoverBandEditModal from '../components/TurnoverBandEditModal';
import { hideEditModal, setTurnoverBand } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';
import { getTurnoverBands, getTurnoverBandSet, getTurnoverBandSetUri, getDiscountScheme } from '../selectors/salesAccountsSelectors';

const mapStateToProps = ({ salesAccount, discountSchemes, turnoverBandSets }, ownProps ) => ({
    visible: salesAccount.editTurnoverBandVisible,
    turnoverBands: getTurnoverBands(getTurnoverBandSet(turnoverBandSets, getTurnoverBandSetUri(getDiscountScheme(discountSchemes, ownProps.discountSchemeUri))))
});

const mapDispatchToProps = {
    hideEditModal,
    setTurnoverBand
};

export default connect(mapStateToProps, mapDispatchToProps)(TurnoverBandEditModal);