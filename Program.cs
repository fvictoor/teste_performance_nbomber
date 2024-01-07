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
        Simulation.Inject(rate: 10, /* Vai inserir o numero de solicita��es solicita��o */
            interval: TimeSpan.FromSeconds(1),/* Intervalo das solicita��es em segundos */
            during: TimeSpan.FromSeconds(30)) /* Valor de dura��o do teste */
    );

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();