import * as actionTypes from '../actions';

const salesAccounts = (state = [], action) => {
    switch (action.type) {
    case actionTypes.REQUEST_SALES_ACCOUNT:
        return state.find(p => p.salesAccountId === action.payload.salesAccountId)
            ? state
            : [
                ...state,
                {
                    salesAccountId: action.payload.salesAccountId,
                    loading: true,
                    item: null
                }
            ];

    case actionTypes.RECEIVE_SALES_ACCOUNT:
    {
        const index = state.findIndex(p => p.salesAccountId === action.payload.salesAccountId);

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
                    salesAccountId: action.payload.salesAccountId,
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