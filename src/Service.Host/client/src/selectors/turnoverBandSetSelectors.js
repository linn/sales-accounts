export const getTurnoverBandSet = (turnoverBandSets, turnoverBandSetUri) => {
    if (!turnoverBandSets || !turnoverBandSetUri) {
        return null;
    }

    return turnoverBandSets.find(ts => ts.links.find(l => l.rel === 'self').href === turnoverBandSetUri);
}
