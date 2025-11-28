using Neurona.Health.Api.Api.GraphQL.Mutations;
using Neurona.Health.Api.Api.GraphQL.Queries;
using Neurona.Health.Api.Application;
using Neurona.Health.Api.Infrastructure.Data;
using GraphQL.Server.Ui.Voyager;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();      // extension v Application layer (registrace IPatientService)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGraphQL("/graphql").WithOptions(new GraphQLServerOptions
{
    Tool = { Enable = true }
});

// Voyager UI – vizualizace schématu
app.UseGraphQLVoyager("/voyager", new VoyagerOptions
{
    GraphQLEndPoint = "/graphql"
});
app.Run();