import * as actionTypes from '../actions';

export const fetchErrorHandlingMiddleware = ({ dispatch, getState }) => next => action => {
    const result = next(action);

    if (action.type === actionTypes.FETCH_ERROR) {
        alert(`Failed to fetch ${action.payload}`);
    }
    
    return result;
}