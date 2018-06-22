import { connect } from 'react-redux';
import ActivityItem from '../components/ActivityItem';
import { getSalesAccountActivityTurnoverBandName, getSalesAccountActivityDiscountSchemeName, getActivityEmployeeName } from '../selectors/utilities/salesAccountSelectorUtilities';

const mapStateToProps = ({ turnoverBandSets, discountSchemes, employees }, props) => ({
    activity: props.activity,
    turnoverBandName: getSalesAccountActivityTurnoverBandName(props.activity, turnoverBandSets),
    discountSchemeName: getSalesAccountActivityDiscountSchemeName(props.activity, discountSchemes),
    updatedByName: getActivityEmployeeName(props.activity, employees)
});

export default connect(mapStateToProps)(ActivityItem);