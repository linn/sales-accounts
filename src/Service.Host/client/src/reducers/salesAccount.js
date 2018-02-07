import * as actionTypes from '../actions';

const defaultState = {
    salesAccountUri: null,
    loading: false,
    editDiscountSchemeVisible: false,
    editTurnoverBandVisible: false,
    editGoodCreditVisible: false,
    item: null,
}

const salesAccount = (state = defaultState, action) => {
    switch (action.type) {
    case actionTypes.REQUEST_SALES_ACCOUNT:
        return {
            ...state,
            salesAccountUri: action.payload.salesAccountUri,
            loading: true
        }
       
    case actionTypes.RECEIVE_SALES_ACCOUNT:
        return {
            ...state,
            loading: false,
            item: action.payload.data
        }

        case actionTypes.SET_DISCOUNT_SCHEME:
        return {
            ...state,
            item: {
                ...state.item, 
                discountSchemeUri: action.payload.discountSchemeUri,
                turnoverBandUri: null,
            }
        }

    case actionTypes.SET_TURNOVER_BAND:
        return {
            ...state,
            item: {
                ...state.item, 
                turnoverBandUri: action.payload.turnoverBandUri,
            }
        }

    case actionTypes.HIDE_EDIT_MODAL:
        return {
            ...state,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
        }
    
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

    case actionTypes.EDIT_GOOD_CREDIT:
        return {
            ...state,
            editGoodCreditVisible: true
        }
    
    default:
        return state;
    }
}

export default salesAccount;