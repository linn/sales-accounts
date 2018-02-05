import * as actionTypes from '../actions';

const defaultState = {
    visible: false
}

const salesAccountEditModal = (state = defaultState, action) => {
    switch (action.type) {
    case actionTypes.HIDE_EDIT_MODAL:
        return defaultState;
    case actionTypes.SHOW_EDIT_MODAL:
        return {
            ...state,
            visible: true
        }
        
    default:
        return state;
    }
}

export default salesAccountEditModal;