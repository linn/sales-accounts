﻿namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;

    public interface ITurnoverBandService
    {
        IResult<TurnoverBandProposal> ProposeTurnoverBands(string financialYear);

        IResult<ProposedTurnoverBand> GetProposedTurnoverBand(int id);

        IResult<TurnoverBandProposal> GetProposedTurnoverBands(string financialYear);

        IResult<IEnumerable<ProposedTurnoverBandModel>> GetProposedTurnoverBandModelResults(string financialYear);

        IResult<ProposedTurnoverBand> OverrideTurnoverBand(int id, string turnoverBandUri);

        IResult<ProposedTurnoverBand> ExcludeFromTurnoverBandProposal(int id);

        IResult<TurnoverBandProposal> ApplyTurnoverBandProposal(string financialYear, string employeeUri);
    }
}