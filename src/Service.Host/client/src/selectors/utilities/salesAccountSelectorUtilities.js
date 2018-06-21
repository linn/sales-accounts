import { getEmployeeName } from "./employeeSelectorUtilities";

export const getActivityEmployeeUris = (salesAccount) => {
    if (!salesAccount || !salesAccount.activities) {
        return null;
    }

    return salesAccount.activities.map(a => a.updatedByUri).filter(a => a != null);
}

export const getSalesAccountItem = (salesAccount) => {    
    if (!salesAccount || !salesAccount.item) {
        return null;
    }

    return salesAccount.item;
}

export const getSalesAccountName = (salesAccount) => {
    if (!salesAccount || !salesAccount.item) {
        return null;
    }

    return salesAccount.item.name;
}

export const getDiscountSchemeUri = (salesAccount) => {
    const salesAccountItem = getSalesAccountItem(salesAccount);

    if (!salesAccountItem) {
        return null;
    }

    return salesAccountItem.discountSchemeUri;
}

export const getSalesAccountActivityTurnoverBandName = (activity, turnoverBandSets) => {
    if (!activity || !activity.turnoverBandUri || !turnoverBandSets) {
        return null;
    }

    return getTurnoverBandName(activity.turnoverBandUri, turnoverBandSets);
}

export const getSalesAccountActivityDiscountSchemeName = (activity, discountSchemes) => {
    if (!activity || !activity.discountSchemeUri || !discountSchemes) {
        return null;
    }

    return getDiscountSchemeName(activity.discountSchemeUri, discountSchemes);
}

export const getTurnoverBandName = (turnoverBandUri, turnoverBandSets) => {
    const allTurnoverBands = turnoverBandSets.reduce((soFar, tbs) => [...soFar, ...tbs.turnoverBands], []);
    const turnoverBand = allTurnoverBands.find(tb => tb.links.find(l => l.rel === 'self').href === turnoverBandUri);

    return turnoverBand ? turnoverBand.name : null;
}

export const getDiscountSchemeName = (discountSchemeUri, discountSchemes) => {
    const discountScheme = discountSchemes.find(s => s.links.find(l => l.href === discountSchemeUri));

    return discountScheme ? discountScheme.name : null;
}

export const getActivityEmployeeName = (activity, employees) => {
    return getEmployeeName(activity.updatedByUri, employees);
}

export const getSalesAccountId = (salesAccount) => {
    if (!salesAccount || !salesAccount.item) {
        return null;
    }

    return salesAccount.item.id;
}