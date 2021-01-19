using DeVes.Bazaar.Dto;

namespace DeVes.Bazaar.Interfaces
{
    public interface IAccountingLogic
    {
        AccountingListDto GetAccountingList(long sellerNumber);

        void ReturnRemainingArticles(long sellerNumber);
    }
}