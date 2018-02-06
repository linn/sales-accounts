import SalesAccountSearch from "../containers/SalesAccountSearch";

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