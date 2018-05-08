import { getTurnoverBandSet } from '../turnoverBandSetSelectors';

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