import * as actionTypes from '../actions';
import { getEmployeesToFetch } from '../selectors/employeeSelectors';
import { getEmployeeName } from '../selectors/utilities/employeeSelectorUtilities'
import { fetchEmployee } from '../actions/employees';

export const activitiesMiddleware = ({dispatch, getState}) => next => action => { 
    const result = next(action);
    
    if (action.type === actionTypes.RECEIVE_ACTIVITIES) {
            const employeesToFetch = getEmployeesToFetch(getState());
            employeesToFetch.length && employeesToFetch.map(e => dispatch(fetchEmployee(e)));
    }

    return result;
}