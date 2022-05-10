using System;

namespace SM_Projekt_WS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient _httpClient;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", "99b8646d-6502-47bc-b564-7f4df54c6d2e");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _httpClient.PutAsync("https://smprojektapi.herokuapp.com/api/Poll/OpenPolls", null);
                if (result.IsSuccessStatusCode)
                {
                        _logger.LogInformation($"Opened polls at: {DateTime.Now}");
                }
                else
                {
                    // TODO: Improve logging
                    _logger.LogError("Error ;(");
                }
                result = await _httpClient.PutAsync("https://smprojektapi.herokuapp.com/api/Poll/ClosePolls", null);
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Closed polls at: {DateTime.Now}");
                }
                else
                {
                    _logger.LogError("Error ;(");
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}