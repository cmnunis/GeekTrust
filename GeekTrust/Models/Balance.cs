namespace geektrust_family_demo.Models
{
    public class Balance
    {
        public Borrower Borrower { get; set; }
        public decimal NumberOfEquatedMonthlyInstallments { get; set; }

        public Balance(Borrower borrower, decimal numberOfEquatedMonthlyInstallments)
        {
            Borrower = borrower;
            NumberOfEquatedMonthlyInstallments = numberOfEquatedMonthlyInstallments;
        }
    }
}
