using NBomber.CSharp;

using var httpClient = new HttpClient();

var scenario = Scenario.Create("Teste de performance", async context =>
{
    var response = await httpClient.GetAsync("https://nbomber.com");

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
    .WithoutWarmUp()
    .WithLoadSimulations(
        Simulation.Inject(rate: 10, /* Vai inserir o numero de solicitações solicitação */
            interval: TimeSpan.FromSeconds(1),/* Intervalo das solicitações em segundos */
            during: TimeSpan.FromSeconds(30)) /* Valor de duração do teste */
    );

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();