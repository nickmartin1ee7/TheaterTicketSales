using System;
using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TheaterTicketSales
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private const decimal ORCHESTRALEVEL_COST = 100m;
        private const decimal GRANDTIER_COST = 75m;
        private const decimal BALCONY_COST = 50m;
        private readonly ICurrencyValidator validator;
        private string orchestraLevelSalesText;

        public MainWindowViewModel()
        {
            CalculateRevenueCommand = new Command(action: o => CalculateRevenue());
            ResetAllCommand = new Command(action: o => ResetAll());
            validator = new PositiveCurrencyValidator();
            ResetAll();
        }

        private void CalculateRevenue()
        {
            SetError(string.Empty, string.Empty);

            if (!validator.Validate(OrchestraLevelSalesText, out var orchestraLevelSales, out var orchestraLevelValidationError))
            {
                SetError("Orchestra Level Sales", orchestraLevelValidationError);
                ResetRevenue();
                return;
            }
            if (!validator.Validate(GrandTierSalesText, out var grandTierSales, out var grandTierValidationError))
            {
                SetError("Grand Tier Sales", grandTierValidationError);
                ResetRevenue();
                return;
            }
            if (!validator.Validate(BalconySalesText, out var balconySales, out var balconyValidationError))
            {
                SetError("Balcony Sales", balconyValidationError);
                ResetRevenue();
                return;
            }

            var orchestraLevelSalesTotal = orchestraLevelSales * ORCHESTRALEVEL_COST;
            var grandTierSalesTotal = grandTierSales * GRANDTIER_COST;
            var balconySalesTotal = balconySales * BALCONY_COST;
            var salesTotal = orchestraLevelSalesTotal + grandTierSalesTotal + balconySalesTotal;

            OrchestraLevelRevenueText = $"{orchestraLevelSalesTotal:C}";
            GrandTierRevenueText = $"{grandTierSalesTotal:C}";
            BalconyRevenueText = $"{balconySalesTotal:C}";
            TotalRevenueText = $"{salesTotal:C}";
        }

        private void SetError(string name, string error)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(error))
            {
                ErrorText = string.Empty;
                return;
            }

            if (!string.IsNullOrEmpty(ErrorText))
            {
                // Error already set
                return;
            }

            ErrorText = $"Error! {name} is not valid because{Environment.NewLine}{error}.";
        }

        private void ResetAll()
        {
            SetError(string.Empty, string.Empty);
            ResetSales();
            ResetRevenue();
        }

        private void ResetRevenue()
        {
            OrchestraLevelRevenueText = $"{0:C}";
            GrandTierRevenueText = $"{0:C}";
            BalconyRevenueText = $"{0:C}";
            TotalRevenueText = $"{0:C}";
        }

        private void ResetSales()
        {
            OrchestraLevelSalesText = string.Empty;
            GrandTierSalesText = string.Empty;
            BalconySalesText = string.Empty;
        }

        public ICommand ResetAllCommand { get; }

        public ICommand CalculateRevenueCommand { get; }

        public string OrchestraLevelSalesText { get => orchestraLevelSalesText; set => SetProperty(ref orchestraLevelSalesText, value); }

        private string grandTierSalesText;

        public string GrandTierSalesText { get => grandTierSalesText; set => SetProperty(ref grandTierSalesText, value); }

        private string balconySalesText;

        public string BalconySalesText { get => balconySalesText; set => SetProperty(ref balconySalesText, value); }

        private string orchestraLevelRevenueText;

        public string OrchestraLevelRevenueText { get => orchestraLevelRevenueText; set => SetProperty(ref orchestraLevelRevenueText, value); }

        private string grandTierRevenueText;

        public string GrandTierRevenueText { get => grandTierRevenueText; set => SetProperty(ref grandTierRevenueText, value); }

        private string balconyRevenueText;

        public string BalconyRevenueText { get => balconyRevenueText; set => SetProperty(ref balconyRevenueText, value); }

        private string errorText;

        public string ErrorText { get => errorText; set => SetProperty(ref errorText, value); }

        private string totalRevenueText;

        public string TotalRevenueText { get => totalRevenueText; set => SetProperty(ref totalRevenueText, value); }
    }
}
