namespace TheaterTicketSales
{
    public class PositiveCurrencyValidator : ICurrencyValidator
    {
        public bool Validate(string input, out decimal amount, out string validationError)
        {
            amount = 0M;
            validationError = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                validationError = $"{nameof(input)} is empty";
                return false;
            }

            if (!decimal.TryParse(input, out amount))
            {
                validationError = $"{nameof(input)} is not a valid number";
                return false;
            }

            if (amount < 0)
            {
                validationError = $"{nameof(input)} cannot be negative";
                return false;
            }

            return true;
        }
    }
}