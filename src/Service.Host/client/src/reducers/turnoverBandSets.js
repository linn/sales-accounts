import * as actionTypes from '../actions';

const turnoverBandSets = (state = [], action) => {
    switch (action.type) {
    case actionTypes.REQUEST_TURNOVER_BAND_SETS:
        return state;

    case actionTypes.RECEIVE_TURNOVER_BAND_SETS:
        return action.payload.data;
    
    default:
        return state;
    }
}

export default turnoverBandSets;