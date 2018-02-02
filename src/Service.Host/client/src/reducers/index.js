import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import salesAccountSearch from './salesAccountSearch';
import salesAccounts from './salesAccounts';

const rootReducer = combineReducers({
    router,
    salesAccountSearch,
    salesAccounts
});

export default rootReducer;