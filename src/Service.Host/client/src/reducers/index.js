import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import { reducer as oidc } from 'redux-oidc';
import salesAccountSearch from './salesAccountSearch';
import salesAccount from './salesAccount';
import salesAccounts from './salesAccounts';
import discountSchemes from './discountSchemes';
import turnoverBandSets from './turnoverBandSets';
import turnoverBandProposal from './turnoverBandProposal';
import employees from './employees';

const rootReducer = combineReducers({
    oidc,
    router,
    salesAccountSearch,
    salesAccount,
    salesAccounts,
    discountSchemes,
    turnoverBandSets,
    turnoverBandProposal,
    employees
});

export default rootReducer;