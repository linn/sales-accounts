import { getEmployeesToFetch } from '../employeeSelectors';

describe('when selecting employee uris to fetch', () => {
    test('should return unique uris', () => {

        const state = {
            salesAccount: {
                loading: false,
                editDiscountSchemeVisible: false,
                editTurnoverBandVisible: false,
                editGoodCreditVisible: false,
                editGrowthPartnerVisible: false,
                editEligibleForRebateVisible: false,
                dirty: false,
                saving: false,
                item: {
                    id: 1,
                    name: 'TeddyBartons',
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            employees: {
                items: []
            }
        }

        const expectedResult = ['/employees/123', '/employees/987'];
        expect(getEmployeesToFetch(state)).toEqual(expectedResult);
    });

    test('should return unique uris not in state', () => {

        const state = {
            salesAccount: {
                loading: false,
                editDiscountSchemeVisible: false,
                editTurnoverBandVisible: false,
                editGoodCreditVisible: false,
                editGrowthPartnerVisible: false,
                editEligibleForRebateVisible: false,
                dirty: false,
                saving: false,
                item: {
                    id: 1,
                    name: 'TeddyBartons',
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            employees: {
                items: [
                    {
                        id: 987,
                        firstName: 'Peter',
                        lastName: 'Beardsley',
                        userName: 'peterb',
                        emailAddress: 'peter.beardsley@linn.co.uk',
                        href: '/employees/987',
                        fullName: 'Peter Beardsley',
                        links: [
                            {
                                href: '/employees/987',
                                rel: 'self'
                            }
                        ]
                    }
                ]
            }
        }

        const expectedResult = ['/employees/123'];
        expect(getEmployeesToFetch(state)).toEqual(expectedResult);
    });

    test('should return none if all in state', () => {

        const state = {
            salesAccount: {
                loading: false,
                editDiscountSchemeVisible: false,
                editTurnoverBandVisible: false,
                editGoodCreditVisible: false,
                editGrowthPartnerVisible: false,
                editEligibleForRebateVisible: false,
                dirty: false,
                saving: false,
                item: {
                    id: 1,
                    name: 'TeddyBartons',
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            employees: {
                items: [
                    {
                        id: 987,
                        firstName: 'Peter',
                        lastName: 'Beardsley',
                        userName: 'peterb',
                        emailAddress: 'peter.beardsley@linn.co.uk',
                        href: '/employees/987',
                        fullName: 'Peter Beardsley',
                        links: [
                            {
                                href: '/employees/987',
                                rel: 'self'
                            }
                        ]
                    },
                    {
                        id: 123,
                        firstName: 'Teddy',
                        lastName: 'Barton',
                        userName: 'teddyb',
                        emailAddress: 'teddy.barton@linn.co.uk',
                        href: '/employees/123',
                        fullName: 'Teddy Barton',
                        links: [
                            {
                                href: '/employees/123',
                                rel: 'self'
                            }
                        ]
                    }
                ]
            }
        }

        const expectedResult = [];
        expect(getEmployeesToFetch(state)).toEqual(expectedResult);
    });
});