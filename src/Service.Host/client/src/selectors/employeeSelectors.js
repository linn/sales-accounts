import { distinct } from '../helpers/utilities';
import { getEmployeeUris } from './utilities/employeeSelectorUtilities';
import { getActivityEmployeeUris } from './salesAccountSelectors';

export const getEmployeesToFetch = ({ salesAccount, employees }) => {
    const urisToFetch = distinct(getActivityEmployeeUris(salesAccount));

    if (!employees.items.length) {
        return urisToFetch;
    }

    return urisToFetch.filter(a => !getEmployeeUris(employees).some(e => e === a));
}