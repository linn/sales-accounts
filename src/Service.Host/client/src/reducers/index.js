import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import salesAccountSearch from './salesAccountSearch';
import salesAccount from './salesAccount';
import discountSchemes from './discountSchemes';
import turnoverBandSets from './turnoverBandSets';

const rootReducer = combineReducers({
    router,
    salesAccountSearch,
    salesAccount,
    discountSchemes,
    turnoverBandSets,
});

export default rootReducer;