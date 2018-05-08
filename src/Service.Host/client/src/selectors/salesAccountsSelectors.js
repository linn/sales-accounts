export const getSalesAccount = (salesAccounts, salesAccountUri) => {
    if(!salesAccounts || !salesAccounts.items){
        return null;
    }

    return salesAccounts.items.find(s => s.item && s.item.uri === salesAccountUri);
}
