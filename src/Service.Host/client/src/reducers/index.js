import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import salesAccountSearch from './salesAccountSearch';
import salesAccounts from './salesAccounts';
import discountSchemes from './discountSchemes';
import turnoverBandSets from './turnoverBandSets';
import salesAccountEditModal from './salesAccountEditModal';

const rootReducer = combineReducers({
    router,
    salesAccountSearch,
    salesAccounts,
    discountSchemes,
    turnoverBandSets,
    salesAccountEditModal
});

export default rootReducer;