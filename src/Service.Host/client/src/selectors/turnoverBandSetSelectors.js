export const getTurnoverBandSet = (turnoverBandSets, turnoverBandSetUri) => {
    if (!turnoverBandSets || !turnoverBandSetUri) {
        return null;
    }

    return turnoverBandSets.find(ts => ts.links.find(l => l.rel === 'self').href === turnoverBandSetUri);
}

export const getTurnoverBandName = (turnoverBandSets, turnoverBandUri) => {
    if (!turnoverBandSets) {
        return null;
    }

    const allTurnoverBands = turnoverBandSets.reduce((soFar, tbs) => [...soFar, ...tbs.turnoverBands], []);
    const turnoverBand = allTurnoverBands.find(tb => tb.links.find(l => l.rel === 'self').href === turnoverBandUri);

    return turnoverBand ? turnoverBand.name : null;
}