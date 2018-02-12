import * as actionTypes from '../actions';

const discountSchemes = (state = [], action) => {
    switch (action.type) {
    case actionTypes.REQUEST_DISCOUNT_SCHEMES:
        return state;

    case actionTypes.RECEIVE_DISCOUNT_SCHEMES:
        return action.payload.data;
    
    default:
        return state;
    }
}

export default discountSchemes;