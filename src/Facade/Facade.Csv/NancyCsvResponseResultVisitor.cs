namespace Linn.SalesAccounts.Facade.Facade.Csv
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Common.Resources;

    using Nancy;
    using Nancy.Configuration;
    using Nancy.Responses.Negotiation;

    public class NancyCsvResponseResultVisitor<T> : IResultVisitor<T, Response>
    {
        private readonly ISerializer serializer;
        private readonly INancyEnvironment environment;
        private readonly IResourceBuilder<T> resourceBuilder;
        private readonly IEnumerable<MediaRange> supportedRanges;

        public NancyCsvResponseResultVisitor(
            INancyEnvironment environment,
            IResourceBuilder<T> resourceBuilder,
            IEnumerable<MediaRange> supportedRanges)
        {
            this.environment = environment;
            this.resourceBuilder = resourceBuilder;
            this.supportedRanges = supportedRanges;
            this.serializer = new CsvSerializer();
        }

        public Response Visit(SuccessResult<T> result)
        {
            var resource = this.resourceBuilder == null ? result.Data : this.resourceBuilder.Build(result.Data);

            return new CsvResponse(resource, this.serializer)
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = this.supportedRanges.Last()
            };
        }

        public Response Visit(UnauthorisedResult<T> result)
        {
            throw new System.NotImplementedException();
        }

        public Response Visit(NotFoundResult<T> result)
        {
            var message = string.IsNullOrEmpty(result.Message) ? "Not found" : result.Message;
            var messageModel = new { message };
            return new CsvResponse(messageModel, this.serializer)
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public Response Visit(CreatedResult<T> result)
        {
            var resource = this.resourceBuilder == null ? result.Data : this.resourceBuilder.Build(result.Data);

            var response = new CsvResponse(resource, this.serializer)
            {
                StatusCode = HttpStatusCode.Created,
                ContentType = this.supportedRanges.Last()
            };

            return response;
        }

        public Response Visit(BadRequestResult<T> result)
        {
            var messageModel = new ErrorResource { Errors = new[] { result.Message } };
            return new CsvResponse(messageModel, this.serializer)
                       {
                           ContentType = MediaTypeBuilder.MediaType("error", 1),
                           StatusCode = HttpStatusCode.BadRequest
                       };
        }

        public Response Visit(ServerFailureResult<T> result)
        {
            var messageModel = new { result.Message };
            return new CsvResponse(messageModel, this.serializer)
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
