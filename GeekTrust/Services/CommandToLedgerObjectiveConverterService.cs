using geektrust_family_demo.Extensions;
using geektrust_family_demo.Models;

namespace geektrust_family_demo.Services
{
    public interface ICommandToLedgerObjectConverterService
    {
        Loan BuildLoanObject(string[] commandStringArray);
        Payment BuildPaymentObject(string[] commandStringArray);
        Balance BuildBalanceObject(string[] commandStringArray);
    }

    public class CommandToLedgerObjectConverterService : ICommandToLedgerObjectConverterService
    {
        public Loan BuildLoanObject(string[] commandStringArray)
        {
            var borrower = new Borrower(commandStringArray[1], commandStringArray[2]);
            return new Loan(borrower, commandStringArray[3].ToDecimal(), commandStringArray[4].ToInt(), commandStringArray[5].ToDecimal());
        }

        public Payment BuildPaymentObject(string[] commandStringArray)
        {
            var borrower = new Borrower(commandStringArray[1], commandStringArray[2]);
            return new Payment(borrower, commandStringArray[3].ToDecimal(), commandStringArray[4].ToDecimal());
        }

        public Balance BuildBalanceObject(string[] commandStringArray)
        {
            var borrower = new Borrower(commandStringArray[1], commandStringArray[2]);
            return new Balance(borrower, commandStringArray[3].ToDecimal());
        }
    }
}
