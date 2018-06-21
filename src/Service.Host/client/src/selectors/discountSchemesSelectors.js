export const getDiscountSchemes = ({ discountSchemes }) => {
    if (!discountSchemes){
        return null;
    }

    return discountSchemes.filter(s => !s.closedOn);
}