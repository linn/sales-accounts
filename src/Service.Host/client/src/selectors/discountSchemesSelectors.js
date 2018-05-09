export const getDiscountSchemes = (discountSchemes) => {
    if (!discountSchemes){
        return null;
    }

    return discountSchemes.filter(s => !s.closedOn);
}

export const getTurnoverBandSetUri = (discountScheme) => {
    if (!discountScheme) {
        return null;
    }

    const bandSet = discountScheme.links.find(l => l.rel === 'turnover-band-set');

    return  bandSet ? bandSet.href : null;
}

export const getDiscountScheme = (discountSchemes, discountSchemeUri) => {
    if (!discountSchemes || !discountSchemeUri) {
        return null;
    }

    return discountSchemes.find(s => s.links.find(l => l.rel === 'self').href === discountSchemeUri) || null;
}