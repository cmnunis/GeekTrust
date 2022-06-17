using System.Threading.Tasks;
using geektrust_family_demo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace geektrust_family_demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            await services
                .AddSingleton(args)
                .AddSingleton<ConsoleService, ConsoleService>()
                .BuildServiceProvider()
                .GetService<ConsoleService>()
                .StartAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<ICommandToLedgerObjectConverterService, CommandToLedgerObjectConverterService>()
                .AddScoped<ICalculatorService, CalculatorService>()
                .AddScoped<IFileReaderService, FileReaderService>()
                .AddScoped<ILoanRepaymentInfoSummaryService, LoanRepaymentInfoSummaryService>()
                .AddScoped<ILoanRepaymentInfoService, LoanRepaymentInfoService>();
        }
    }
}
