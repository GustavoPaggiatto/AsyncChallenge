using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using log4net;
using Microsoft.Extensions.Hosting;

namespace Api.Workers
{
    public sealed class StudentSignUpWorker : IHostedService
    {
        private readonly IStudentService _studentService;
        private readonly ILog _logger;

        public StudentSignUpWorker(IStudentService studentService, ILog logger)
        {
            this._studentService = studentService;
            this._logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    this._logger.Debug("Initializing new time cycle to read students signUps queue.");

                    this._studentService.ProccessSignUp();

                    this._logger.Debug("Finalizing new time cycle to read students signUps queue.");
                    
                    Thread.Sleep(60000);
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}