namespace TheaterTicketSales
{
    public interface ICurrencyValidator
    {
        bool Validate(string input, out decimal amount, out string validationError);
    }
}