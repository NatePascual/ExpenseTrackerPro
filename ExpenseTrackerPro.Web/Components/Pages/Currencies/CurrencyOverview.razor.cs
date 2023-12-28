using ExpenseTrackerPro.Application.Features.Currencies;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Currencies;

public partial class CurrencyOverview
{
    private GetCurrencyView? list;
    private IEnumerable<GetCurrencyResponse> pagedData;
    private IEnumerable<GetCurrencyResponse> data;
    private MudTable<GetCurrencyResponse> table;

    private int totalItems;
    private string searchString = null;

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<GetCurrencyResponse>> ServerReload(TableState state)
    {
        await LoadData();

        await SortAndPaged(state);

        return new TableData<GetCurrencyResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData()
    {
        list = await mediator.Send(new GetCurrencyQuery(""));

        data = list.Currencies.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (item.CountryCurrency.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.Code.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "countryCurrency":
                data = data.OrderByDirection(state.SortDirection, o => o.CountryCurrency);
                break;
            case "code":
                data = data.OrderByDirection(state.SortDirection, o => o.Code);
                break;
        }

        pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
    }

    private void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }
}
