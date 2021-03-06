using geektrust_family_demo.Constants;
using geektrust_family_demo.Extensions;
using geektrust_family_demo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace geektrust_family_demo.Services
{
    public interface ILoanRepaymentInfoService
    {
        Task GenerateLoanRepaymentInfoAsync(string[] args);
    }

    public class LoanRepaymentInfoService : ILoanRepaymentInfoService
    {
        private readonly ICommandToLedgerObjectConverterService _commandToLedgerObjectConverter;
        private readonly IFileReaderService _fileReaderService;
        private readonly ILoanRepaymentInfoSummaryService _loanRepaymentInfoSummaryService;

        public LoanRepaymentInfoService(ICommandToLedgerObjectConverterService commandToLedgerObjectConverter, IFileReaderService fileReaderService, ILoanRepaymentInfoSummaryService loanRepaymentInfoSummaryService)
        {
            _commandToLedgerObjectConverter = commandToLedgerObjectConverter ?? throw new ArgumentNullException(nameof(commandToLedgerObjectConverter));
            _fileReaderService = fileReaderService ?? throw new ArgumentNullException(nameof(fileReaderService));
            _loanRepaymentInfoSummaryService = loanRepaymentInfoSummaryService ?? throw new ArgumentNullException(nameof(loanRepaymentInfoSummaryService));
        }

        public async Task GenerateLoanRepaymentInfoAsync(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            foreach (var arg in args)
            {
                var loans = new List<Loan>();
                var payments = new List<Payment>();
                var balanceQueries = new List<Balance>();

                var fileContents = await _fileReaderService.GetFileContentsAsync(arg);

                GetDataFromFileContents(loans, payments, balanceQueries, fileContents);

                Console.WriteLine($"\r\n");
                var loanRepaymentInfoSummary = _loanRepaymentInfoSummaryService.GenerateLoanRepaymentInfoSummary(loans, payments, balanceQueries);
                var output = _loanRepaymentInfoSummaryService.OutputLoanRepaymentInfoSummary(loanRepaymentInfoSummary);
                Console.WriteLine(output);
            }
        }

        private void BuildDataSourceFromCommands(string[] commandStringArray, List<Loan> loans, List<Payment> payments, List<Balance> balanceQueries)
        {
            if (commandStringArray[0].Equals(Commands.Loan, StringComparison.OrdinalIgnoreCase))
            {
                var loan = _commandToLedgerObjectConverter.BuildLoanObject(commandStringArray);
                loans.Add(loan);
            }
            else if (commandStringArray[0].Equals(Commands.Payment, StringComparison.OrdinalIgnoreCase))
            {
                var payment = _commandToLedgerObjectConverter.BuildPaymentObject(commandStringArray);
                payments.Add(payment);
            }
            else if (commandStringArray[0].Equals(Commands.Balance, StringComparison.OrdinalIgnoreCase))
            {
                var balance = _commandToLedgerObjectConverter.BuildBalanceObject(commandStringArray);
                balanceQueries.Add(balance);
            }
        }

        private void GetDataFromFileContents(List<Loan> loans, List<Payment> payments, List<Balance> balanceQueries, string[] fileContents)
        {
            if (fileContents == null)
            {
                return;
            }

            foreach (var line in fileContents)
            {
                var commandStringArray = line.SplitCommand();

                BuildDataSourceFromCommands(commandStringArray, loans, payments, balanceQueries);
            }
        }
    }
}