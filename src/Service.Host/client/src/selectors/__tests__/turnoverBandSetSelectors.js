import { getTurnoverBandSet, getTurnoverBandName } from '../turnoverBandSetSelectors';

describe('when getting turnover band set', () => {
    test('should return set', () => {

        const turnoverBandSets = [
            { links: [{ rel: 'self', href: '/1' }] },
            { links: [{ rel: 'self', href: '/2' }] }
        ];


        const expectedResult = { links: [{ rel: 'self', href: '/1' }] };

        expect(getTurnoverBandSet(turnoverBandSets, '/1')).toEqual(expectedResult);
    });
});

describe('when getting turnover band set before data is loaded', () => {
    test('should return null', () => {
        expect(getTurnoverBandSet(null, '/1')).toEqual(null);
    });
});

describe('when getting turnover band name', () => {
    test('should return name', () => {

        const turnoverBandSets = [
            {
                links: [{ rel: 'self', href: '/1' }],
                turnoverBands: [
                    { name: 'tb1', links: [{ rel: 'self', href: '/1' }] },
                    { name: 'tb2', links: [{ rel: 'self', href: '/2' }] }]
            },
            {
                links: [{ rel: 'self', href: '/2' }],
                turnoverBands: [
                    { name: 'tb3', links: [{ rel: 'self', href: '/3' }] },
                    { name: 'tb4', links: [{ rel: 'self', href: '/4' }] }] }
        ];


        const expectedResult = 'tb3';

        expect(getTurnoverBandName(turnoverBandSets, '/3')).toEqual(expectedResult);
    });
});

describe('when getting turnover band name before data is loaded', () => {
    test('should return null', () => {
        expect(getTurnoverBandName(null, '/1')).toEqual(null);
    });
});