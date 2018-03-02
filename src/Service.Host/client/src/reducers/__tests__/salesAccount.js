import salesAccount from '../salesAccount';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('sales accounts reducer', () => {
    test('when requesting an account', () => {
        const state =  {
            salesAccountUri: null,
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: null
        };

        const action = {
            type: actionTypes.REQUEST_SALES_ACCOUNT,
            payload: {
                salesAccountUri: '/sales/accounts/1'
            }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: true,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: null
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when receiving a sales account', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: true,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: null
        };

        const action = {
            type: actionTypes.RECEIVE_SALES_ACCOUNT,
            payload: {
                data : {
                    id: 1,
                    name: 'TeddyBartons',
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                    discountSchemeUri: '/sales/discounting/schemes/1',
                    eligibleForGoodCreditDiscount: false,
                    eligibleForRebate: false,
                    growthPartner: false,
                    closedOn: "",
                    address: {
                        line1: '34',
                        line2: 'Street',
                        countryUri: '/countries/1',
                        postcode: 'KY128ES'
                    }            
                }
            }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {
                    line1: '34',
                    line2: 'Street',
                    countryUri: '/countries/1',
                    postcode: 'KY128ES'
                }                       
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when hiding edit modals', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: true,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        const action = {
            type: actionTypes.HIDE_EDIT_MODAL,
            payload: {}
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when editing discount scheme it should set visible to true', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}           
            }
        };

        const action = {
            type: actionTypes.EDIT_DISCOUNT_SCHEME,
            payload: { }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: true,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}           
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when setting discount scheme it should update uri', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: true,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        const action = {
            type: actionTypes.SET_DISCOUNT_SCHEME,
            payload: { discountSchemeUri : '/sales/discounting/schemes/2' }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: true,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: null,
                discountSchemeUri: '/sales/discounting/schemes/2',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when editing turnover band it should set visible to true', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        const action = {
            type: actionTypes.EDIT_TURNOVER_BAND,
            payload: { }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: true,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when setting turnover band it should update uri', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: true,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        const action = {
            type: actionTypes.SET_TURNOVER_BAND,
            payload: { turnoverBandUri : '/sales/discounting/turnover-band-sets/1/turnover-bands/2' }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: true,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/2',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when editing good credit discount it should set visible to true', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        const action = {
            type: actionTypes.EDIT_ELIGIBLE_FOR_GOOD_CREDIT_DISCOUNT,
            payload: { }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: true,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when setting good credit discount it should change boolean', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: true,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        const action = {
            type: actionTypes.SET_ELIGIBLE_FOR_GOOD_CREDIT_DISCOUNT,
            payload: { eligible : true}
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: true,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: true,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when editing growth partner should set visible to true', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        const action = {
            type: actionTypes.EDIT_GROWTH_PARTNER,
            payload: { }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: true,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when setting growth partner it should change boolean', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: true,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        const action = {
            type: actionTypes.SET_GROWTH_PARTNER,
            payload: { eligible : true}
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: true,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: true,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when eligible for rebate should set visible to true', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        const action = {
            type: actionTypes.EDIT_ELIGIBLE_FOR_REBATE,
            payload: { }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: true,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when setting eligible for rebate it should change boolean', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: true,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        const action = {
            type: actionTypes.SET_ELIGIBLE_FOR_REBATE,
            payload: { eligible : true}
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: true,
            dirty: true,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: true,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when saving should set saving true', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        const action = {
            type: actionTypes.START_SAVE,
            payload: { saving : true }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: true,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when saving complete should set saving false and dirty false', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: true,
            saving: true,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        const action = {
            type: actionTypes.SAVE_COMPLETE,
            payload: { saving : true, dirty: false }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '',
                address: {}            
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when requesting a country name', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {
                    line1: '34',
                    line2: 'Street',
                    countryUri: '/countries/1',
                    postcode: 'KY128ES'
                }            
            }
        };

        const action = {
            type: actionTypes.REQUEST_COUNTRY,
            payload: { }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
            loading: true,
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {
                    line1: '34',
                    line2: 'Street',
                    countryUri: '/countries/1',
                    postcode: 'KY128ES',
                    countryName: null
                }                       
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

    test('when receiving a country name', () => {
        const state = {
            salesAccountUri: '/sales/accounts/1',
            loading: true,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item:  {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {
                    line1: '34',
                    line2: 'Street',
                    countryUri: '/countries/1',
                    postcode: 'KY128ES',
                    countryName: null
                }            
            }
        };

        const action = {
            type: actionTypes.RECEIVE_COUNTRY,
            payload: {
                data : {
                    "countryCode": "MA",
                    "countryName": "Morocco",
                    "links": [
                        {
                            "href": "/countries/MA",
                            "rel": "self"
                        }
                    ]
                },
            }
        };

        const expected = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: "",
                address: {
                    line1: '34',
                    line2: 'Street',
                    countryUri: '/countries/1',
                    postcode: 'KY128ES',
                    countryName: 'Morocco'
                }                       
            }
        };

        deepFreeze(state);

        expect(salesAccount(state, action)).toEqual(expected);
    });

});