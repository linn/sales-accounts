import { getFinancialYear, getProposedTurnoverBands, getLoading } from '../turnoverBandProposalSelectors';

describe('when getting financial year', () => {
    test('should return financial year', () => {
       
        const turnoverBandProposal = {
            financialYear : '2050'
        };

        const expectedResult = '2050';

        expect(getFinancialYear(turnoverBandProposal)).toEqual(expectedResult);
    });
});

describe('when getting financial year before data is loaded', () => {
    test('should return null', () => {
        expect(getFinancialYear(null)).toEqual(null);
    });
});

describe('when getting proposed turnover bands', () => {
    test('should return proposed turnover bands', () => {

        const turnoverBandProposal = {
            financialYear: '2050',
            proposedTurnoverBands: [{ id : 1 }]
        };

        const expectedResult = [{ id: 1 }];

        expect(getProposedTurnoverBands(turnoverBandProposal)).toEqual(expectedResult);
    });
});

describe('when getting proposed turnover bands before data is loaded', () => {
    test('should return null', () => {
        expect(getProposedTurnoverBands(null)).toEqual(null);
    });
});

describe('when getting loading', () => {
    test('should return loading', () => {

        const turnoverBandProposal = {
            financialYear: '2050',
            proposedTurnoverBands: [{ id: 1 }],
            loading : true
        };

        expect(getLoading(turnoverBandProposal)).toEqual(true);
    });
});

describe('when getting proposed turnover bands before data is loaded', () => {
    test('should return null', () => {
        expect(getLoading(null)).toEqual(null);
    });
});