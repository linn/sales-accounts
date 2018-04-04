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

export const getDiscountSchemeClosedOn = (salesAccount, discountSchemes) => {
    if (!salesAccount || !discountSchemes) {
        return null;
    }

    const discountScheme = discountSchemes.find(s => s.links.find(l => l.href === salesAccount.discountSchemeUri));
    
    return discountScheme ? discountScheme.closedOn : null;
}

export const getTurnoverBandName = (salesAccount, turnoverBandSets) => {

    if (!turnoverBandSets || !salesAccount){
        return null;
    }

    const allTurnoverBands = turnoverBandSets.reduce((soFar, tbs) => [...soFar, ...tbs.turnoverBands], []);     

    const turnoverBand = allTurnoverBands.find(tb => tb.links.find(l => l.rel === 'self').href === salesAccount.turnoverBandUri);

    return turnoverBand ? turnoverBand.name : null;
}

export const getDiscountSchemes = (discountSchemes) => {
    if (!discountSchemes){
        return null;
    }

    return discountSchemes.filter(s => s.closedOn == '');
}

const getDiscountSchemeUri = (salesAccount) => {
    if (!salesAccount) {
        return null;
    }
    return salesAccount.discountSchemeUri;
}

const getTurnoverBandSetUri = (discountScheme) => {
    if (!discountScheme) {
        return null;
    }

    const bandSet = discountScheme.links.find(l => l.rel === 'turnover-band-set');

    return  bandSet ? bandSet.href : null;
}

const getDiscountScheme = (discountSchemes, discountSchemeUri) => {
    if (!discountSchemes || !discountSchemeUri) {
        return null;
    }
    return discountSchemes.find(s => s.links.find(l => l.rel === 'self').href === discountSchemeUri) || null;
}

const getTurnoverBandSet = (turnoverBandSets, turnoverBandSetUri) => {
    if (!turnoverBandSets || !turnoverBandSetUri)  {
        return null;
    }
    const turnoverBandSet = turnoverBandSets.find(ts => ts.links.rel === 'self');
    return turnoverBandSets.find(ts => ts.links.find(l =>l.rel === 'self').href === turnoverBandSetUri);
}

export const getTurnoverBands = (salesAccount, turnoverBandSets, discountSchemes) => { 
    if (!salesAccount || !turnoverBandSets || !discountSchemes) {
        return null;
    }

    const turnoverBandSet = getTurnoverBandSet(turnoverBandSets, getTurnoverBandSetUri(getDiscountScheme(discountSchemes, getDiscountSchemeUri(getSalesAccount(salesAccount)))));
    
    return turnoverBandSet ? turnoverBandSet.turnoverBands : null;
}


