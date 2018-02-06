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