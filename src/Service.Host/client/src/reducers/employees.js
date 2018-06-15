import * as actionTypes from '../actions';
import { distinct } from '../helpers/utilities';

const defaultState = {
    loading: false,
    items: []
}

const employees = (state = defaultState, action) => {
    switch (action.type) {
        case actionTypes.REQUEST_EMPLOYEE:
            {
                const employeeToAdd = {
                    href: action.payload.employeeUri,
                    loading: true
                }

                return {
                    ...state,
                    loading: true,
                    items: [...state.items, employeeToAdd]
                }
            }

        case actionTypes.RECEIVE_EMPLOYEE:
            {
                const employees = state.items.filter(e => e.href !== action.payload.data.href);

                const employeeToAdd = action.payload.data;
                employeeToAdd.loading = false;

                return {
                    ...state,
                    loading: false,
                    items: [...employees, employeeToAdd]
                }
            }

        default:
            return state;
    }
}

export default employees;