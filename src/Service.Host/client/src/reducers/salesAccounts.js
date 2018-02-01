import * as actionTypes from '../actions';

const salesAccounts = (state = [], action) => {
    switch (action.type) {
    case actionTypes.REQUEST_SALES_ACCOUNT:
        return state.find(p => p.salesAccountUri === action.payload.salesAccountUri)
            ? state
            : [
                ...state,
                {
                    salesAccountUri: action.payload.salesAccountUri,
                    loading: true,
                    item: null
                }
            ];

    case actionTypes.RECEIVE_SALES_ACCOUNT:
    {
        const index = state.findIndex(p => p.salesAccountUri === action.payload.salesAccountUri);

        return (index > -1)
            ? [
                ...state.slice(0, index),
                {
                    ...state[index],
                    loading: false,
                    item: action.payload.salesAccount
                },
                ...state.slice(index + 1)
            ]
            : [
                ...state,
                {
                    salesAccountUri: action.payload.salesAccountUri,
                    loading: false,
                    item: action.payload.salesAccount
                }
            ];
    }

    default:
        return state;
    }
}

export default salesAccounts;