using SmartSaver.Presentation.Pages.Transactions;

namespace SmartSaver.Presentation.Models
{
    public class TransactionsSortingModel
    {
        public TransactionsSortingEnum SortingColumn { get; set; } = TransactionsSortingEnum.CreatedAt;

        public bool IsAscending { get; set; } = true;
    }
}