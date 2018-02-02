export const getSelfHref = itemWithLinks => {
    return getHref(itemWithLinks, 'self');
}

export const getHref = (itemWithLinks, rel) => {
    if (itemWithLinks && itemWithLinks.links && itemWithLinks.links.length > 0) {
        const link = itemWithLinks.links.find(l => l.rel === rel);

        return link ? link.href : null;
    }

    return null;
}

export const roundToTwoDecimalPlaces = value => Math.round(value * 100) / 100;

export const formatToTwoDecimalPlace = value => roundToTwoDecimalPlaces(value).toFixed(2);

export const formatWithCommasAndTwoDecimalPlaces = value => formatWithCommas(value, 2);

export const formatWithCommas = (value, decimalPlaces) => {
    return value &&
        value.toLocaleString(undefined,
            {
                minimumFractionDigits: decimalPlaces,
                maximumFractionDigits: decimalPlaces
            });
};

export const withPercentSign = value => {
    return value
        ? `${value}%`
        : value;
};

export const compareArrays = (a1 = [], a2 = []) => {
    return a1.length === a2.length && a1.every(i => a2.includes(i)) && a2.every(i => a1.includes(i));
};

export const getUnique = (arr = []) => arr.reduce((soFar, item) => soFar.includes(item) ? soFar : [...soFar, item], []);

export const parseFloatWithFallback = (str, defaultValue = 0) => {
    var result = parseFloat(str);

    return Number.isNaN(result)
        ? defaultValue
        : result;
}

export const distinct = (array) => {
    return Array.from(new Set(array));
}