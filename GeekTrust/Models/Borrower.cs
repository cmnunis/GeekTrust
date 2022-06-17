namespace geektrust_family_demo.Models
{
    public class Borrower
    {
        public string BankName { get; set; }
        public string BorrowerName { get; set; }

        public Borrower(string bankName, string borrowerName)
        {
            BankName = bankName;
            BorrowerName = borrowerName;
        }
    }
}
