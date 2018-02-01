import * as actionTypes from '../actions';

const defaultState = {
    visible: true,
    loading: false,
    searchTerm: '',
    items: []
}

const salesAccountSearch = (state = defaultState, action) => {
    switch (action.type) {
    case actionTypes.REQUEST_SALES_ACCOUNTS:
        return {
            ...state,
            loading: true,
            searchTerm: action.payload.searchTerm,
            items: []
        }

    case actionTypes.RECEIVE_SALES_ACCOUNTS:
        return {
            ...state,
            loading: false,
            searchTerm: action.payload.searchTerm,
            items: action.payload.salesAccounts
        }

    case actionTypes.SHOW_SALES_ACCOUNT_SEARCH:
        return {
            ...defaultState,
            visible: true
        }

    case actionTypes.HIDE_SALES_ACCOUNT_SEARCH:
        return {
            ...defaultState,
            visible: false
        }

    default:
        return state;
    }
}

export default salesAccountSearch;