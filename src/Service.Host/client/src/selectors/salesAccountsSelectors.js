﻿import SalesAccountSearch from "../containers/SalesAccountSearch";
import { toUnicode } from "punycode";

export const getSalesAccount = (salesAccount) => {
    if(!salesAccount || !salesAccount.item){
        return null;
    }
    return salesAccount.item;
}

export const getDiscountSchemeName = (salesAccount, discountSchemes) => {
    if (!salesAccount || !discountSchemes) {
        return null;
    }

    var discountScheme = discountSchemes.find(s => s.links.find(l => l.href === salesAccount.discountSchemeUri));

    return discountScheme ? discountScheme.name : null;
}

export const getDiscountScheme = (discountSchemes, discountSchemeUri) => {
    if (!discountSchemes || !discountSchemeUri) {
        return null;
    }
    return discountSchemes.find(s => s.links.find(l => l.rel === 'self').href === discountSchemeUri) || null;
}

export const getDiscountSchemeUri = (salesAccount) => {
    if (!salesAccount || !salesAccount.item) {
        return null;
    }
    return salesAccount.item.discountSchemeUri;
}

export const getTurnoverBandSetUri = (discountScheme) => {
    if (!discountScheme) {
        return null;
    }
    return discountScheme.links.find(l => l.rel === 'turnover-band-set').href;
}

export const getTurnoverBandSet = (turnoverBandSets, turnoverBandSetUri) => {
    if (!turnoverBandSets || !turnoverBandSetUri)  {
        return null;
    }
    const turnoverBandSet = turnoverBandSets.find(ts => ts.links.rel === 'self');
    return turnoverBandSets.find(ts => ts.links.find(l =>l.rel === 'self').href === turnoverBandSetUri);
}

export const getTurnoverBands = (turnoverBandSet) => {
    if (!turnoverBandSet) {
        return null;
    }
    return turnoverBandSet.turnoverBands;
}

export const getTurnoverBandName = (turnoverBandSets, turnoverBandUri) => {
    
    if (!turnoverBandUri || !turnoverBandSets) {
        return null;
    }

    const allTurnoverBands = turnoverBandSets.reduce((soFar, tbs) => [...soFar, ...tbs.turnoverBands], []);     

    const turnoverBand = allTurnoverBands.find(tb => tb.links.find(l => l.rel === 'self').href === turnoverBandUri);

    return turnoverBand ? turnoverBand.name : null;
}

export const getTurnoverBandName2 = (salesAccount, turnoverBandSets) => {
   
    if (!turnoverBandSets || !salesAccount){
        return null;
    }

    const allTurnoverBands = turnoverBandSets.reduce((soFar, tbs) => [...soFar, ...tbs.turnoverBands], []);     

    const turnoverBand = allTurnoverBands.find(tb => tb.links.find(l => l.rel === 'self').href === salesAccount.turnoverBandUri);


    return turnoverBand ? turnoverBand.name : null;
}