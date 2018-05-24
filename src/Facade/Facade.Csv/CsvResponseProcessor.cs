namespace Linn.SalesAccounts.Facade.Facade.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    using Nancy;
    using Nancy.Responses.Negotiation;

    public abstract class CsvResponseProcessor<T> : IResponseProcessor
    {
        private readonly IResourceBuilder<T> resourceBuilder;

        private readonly IEnumerable<MediaRange> supportedRanges;

        protected CsvResponseProcessor(IResourceBuilder<T> resourceBuilder)
        {
            this.resourceBuilder = resourceBuilder;
            this.supportedRanges = new[] { new MediaRange("text/csv") };
        }

        protected CsvResponseProcessor(IResourceBuilder<T> resourceBuilder, IEnumerable<MediaRange> supportedRanges)
        {
            this.resourceBuilder = resourceBuilder;
            this.supportedRanges = supportedRanges;
        }

        protected CsvResponseProcessor(IResourceBuilder<T> resourceBuilder, string mediaType, int? version = null)
        {
            this.resourceBuilder = resourceBuilder;
            this.supportedRanges = MediaTypeBuilder.MediaRanges(mediaType, version, null, "csv", MediaTypeBuilder.CsvType);
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings => Enumerable.Empty<Tuple<string, MediaRange>>();

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            var modelMatch = model is IResult<T>;
            var mediaMatch = this.supportedRanges.Any(r => r.MatchesWithParameters(requestedMediaRange));

            return new ProcessorMatch
            {
                ModelResult = modelMatch ? MatchResult.ExactMatch : MatchResult.NoMatch,
                RequestedContentTypeResult = mediaMatch ? MatchResult.ExactMatch : MatchResult.NoMatch
            };
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            var result = (IResult<T>)model;

            var visitor = new NancyCsvResponseResultVisitor<T>(context.Environment, this.resourceBuilder, this.supportedRanges);

            var response = result.Accept(visitor);
            response.Headers.Add("Content-Disposition", "attachment; filename=" + $"Export-{DateTime.Now:dd-MM-yyyy-HH-mm}.csv");

            return response;
        }
    }
}