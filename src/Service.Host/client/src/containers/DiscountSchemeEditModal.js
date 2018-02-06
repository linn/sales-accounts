import { connect } from 'react-redux';
import DiscountSchemeEditModal from '../components/DiscountSchemeEditModal';
import { hideEditModal, setDiscountScheme } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets, fetchTurnoverBand } from '../actions/turnoverBandSets';

const mapStateToProps = ({ salesAccountEdit, discountSchemes  }, ownProps ) => ({
    visible: salesAccountEdit.editDiscountSchemeVisible,
    discountSchemes
});

const mapDispatchToProps = {
    hideEditModal,
    setDiscountScheme
};

export default connect(mapStateToProps, mapDispatchToProps)(DiscountSchemeEditModal);