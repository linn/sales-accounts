import { getDiscountScheme, getDiscountSchemes, getTurnoverBandSetUri } from './discountSchemesSelectors';
import { getTurnoverBandSet } from './turnoverBandSetSelectors';

export const getSalesAccount = (salesAccount) => {
    if(!salesAccount || !salesAccount.item){
        return null;
    }

    return salesAccount.item;
}

export const getSalesAccountName = (salesAccount) => {
    if (!salesAccount) {
        return null;
    }

    return salesAccount.name;
}

export const getSalesAccountId = (salesAccount) => {
    if (!salesAccount) {
        return null;
    }

    return salesAccount.id;
}

export const getDiscountSchemeName = (salesAccount, discountSchemes) => {
    if (!salesAccount || !discountSchemes) {
        return null;
    }

    var discountScheme = discountSchemes.find(s => s.links.find(l => l.href === salesAccount.discountSchemeUri));

    return discountScheme ? discountScheme.name : null;
}

export const getDiscountSchemeClosedOn = (salesAccount, discountSchemes) => {
    if (!salesAccount || !discountSchemes) {
        return null;
    }

    const discountScheme = discountSchemes.find(s => s.links.find(l => l.href === salesAccount.discountSchemeUri));
    
    return discountScheme ? discountScheme.closedOn : null;
}

export const getSalesAccountTurnoverBandName = (salesAccount, turnoverBandSets) => {
    if (!turnoverBandSets || !salesAccount){
        return null;
    }

    const allTurnoverBands = turnoverBandSets.reduce((soFar, tbs) => [...soFar, ...tbs.turnoverBands], []);     
    const turnoverBand = allTurnoverBands.find(tb => tb.links.find(l => l.rel === 'self').href === salesAccount.turnoverBandUri);

    return turnoverBand ? turnoverBand.name : null;
}

const getDiscountSchemeUri = (salesAccount) => {
    if (!salesAccount) {
        return null;
    }

    return salesAccount.discountSchemeUri;
}

export const getTurnoverBands = (salesAccount, turnoverBandSets, discountSchemes) => { 
    if (!salesAccount || !turnoverBandSets || !discountSchemes) {
        return null;
    }

    const turnoverBandSet = getTurnoverBandSet(turnoverBandSets, getTurnoverBandSetUri(getDiscountScheme(discountSchemes, getDiscountSchemeUri(salesAccount))));
    
    return turnoverBandSet ? turnoverBandSet.turnoverBands : null;
}

export const getActivities = (salesAccount) => {
    if (!salesAccount || !salesAccount.activities) {
        return null;
    }

    return salesAccount.activities;
}

