import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import salesAccountSearch from './salesAccountSearch';

const rootReducer = combineReducers({
    router,
    salesAccountSearch
});

export default rootReducer;