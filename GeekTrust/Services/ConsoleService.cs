using System;
using System.Threading.Tasks;

namespace geektrust_family_demo.Services
{
    public class ConsoleService
    {
        private readonly ILoanRepaymentInfoService _loanRepaymentInfoService;
        private readonly string[] _args;

        public ConsoleService(ILoanRepaymentInfoService loanRepaymentInfoService, string[] args)
        {
            _loanRepaymentInfoService = loanRepaymentInfoService ?? throw new ArgumentNullException(nameof(loanRepaymentInfoService));
            _args = args ?? throw new ArgumentNullException(nameof(args));
        }

        public async Task StartAsync()
        {
            try
            {
                 await _loanRepaymentInfoService.GenerateLoanRepaymentInfoAsync(_args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
