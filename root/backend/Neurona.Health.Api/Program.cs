using Neurona.Health.Api.Api.GraphQL.Mutations;
using Neurona.Health.Api.Api.GraphQL.Queries;
using Neurona.Health.Api.Application;
using Neurona.Health.Api.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();      // extension v Application layer (registrace IPatientService)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL("/graphql");
app.Run();