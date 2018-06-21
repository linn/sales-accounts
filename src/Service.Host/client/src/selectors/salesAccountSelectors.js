import { getDiscountSchemes } from './discountSchemesSelectors';
import { getTurnoverBandSetUri, getDiscountScheme } from './utilities/discountSchemeSelectorUtilities';
import { getTurnoverBandSet } from './turnoverBandSetSelectors';
import { getEmployeeName, getEmployeesLoading } from './utilities/employeeSelectorUtilities';
import { getSalesAccountItem, getDiscountSchemeUri, getDiscountSchemeName, getTurnoverBandName } from './utilities/salesAccountSelectorUtilities';

export const getSalesAccount = ({ salesAccount }) => {
    return getSalesAccountItem(salesAccount);
}

export const getSalesAccountDiscountSchemeName = ({ salesAccount, discountSchemes }) => {
    const salesAccountItem = getSalesAccountItem(salesAccount);

    if (!salesAccountItem || !salesAccountItem.discountSchemeUri || !discountSchemes) {
        return null;
    }

    return getDiscountSchemeName(salesAccountItem.discountSchemeUri, discountSchemes);
}

export const getDiscountSchemeClosedOn = ({ salesAccount, discountSchemes }) => {    
    const salesAccountItem = getSalesAccountItem(salesAccount);
    
    if (!salesAccountItem || !discountSchemes) {
        return null;
    }

    const discountScheme = discountSchemes.find(s => s.links.find(l => l.href === salesAccountItem.discountSchemeUri));

    return discountScheme ? discountScheme.closedOn : null;
}

export const getSalesAccountTurnoverBandName = ({ salesAccount, turnoverBandSets }) => {
    const salesAccountItem = getSalesAccountItem(salesAccount);

    if (!salesAccountItem || !salesAccountItem.turnoverBandUri || !turnoverBandSets) {
        return null;
    }

    return getTurnoverBandName(salesAccountItem.turnoverBandUri, turnoverBandSets);
}

export const getTurnoverBands = ({ salesAccount, turnoverBandSets, discountSchemes }) => {
    if (!salesAccount || !turnoverBandSets || !discountSchemes) {
        return null;
    }

    const turnoverBandSet = getTurnoverBandSet(turnoverBandSets, getTurnoverBandSetUri(getDiscountScheme(discountSchemes, getDiscountSchemeUri(salesAccount))));

    return turnoverBandSet ? turnoverBandSet.turnoverBands : null;
}

export const getSalesAccountActivities = ({ salesAccount }) => {
    if (!salesAccount || !salesAccount.activities) {
        return null;
    }

    return salesAccount.activities;
}

export const getSalesAccountLoading = ({ salesAccount, discountSchemes, turnoverBandSets, employees }) => {
    return salesAccount.loading || !discountSchemes || !turnoverBandSets || getEmployeesLoading(employees);
}