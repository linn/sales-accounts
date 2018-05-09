import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import salesAccountSearch from './salesAccountSearch';
import salesAccount from './salesAccount';
import salesAccounts from './salesAccounts';
import discountSchemes from './discountSchemes';
import turnoverBandSets from './turnoverBandSets';
import turnoverBandProposal from './turnoverBandProposal';

const rootReducer = combineReducers({
    router,
    salesAccountSearch,
    salesAccount,
    salesAccounts,
    discountSchemes,
    turnoverBandSets,
    turnoverBandProposal
});

export default rootReducer;