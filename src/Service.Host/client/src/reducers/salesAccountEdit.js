import * as actionTypes from '../actions';

const defaultState = {
    editDiscountSchemeVisible: false,
    editTurnoverBandVisible: false
}

const salesAccountEdit = (state = defaultState, action) => {
    switch (action.type) {
    case actionTypes.HIDE_EDIT_MODAL:
        return defaultState;
    case actionTypes.SHOW_DISCOUNT_SCHEME_EDIT_MODAL:
        return {
            ...state,
            editDiscountSchemeVisible: true
        }

    case actionTypes.SHOW_TURNOVER_BAND_EDIT_MODAL:
        return {
            ...state,
            editTurnoverBandVisible: true
        }
        
    default:
        return state;
    }
}

export default salesAccountEdit;