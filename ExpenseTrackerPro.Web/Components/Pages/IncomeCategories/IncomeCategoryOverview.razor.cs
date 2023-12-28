using ExpenseTrackerPro.Application.Features.IncomeCategories;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.IncomeCategories;

public partial class IncomeCategoryOverview
{
    private GetIncomeCategoryView? list;
    private IEnumerable<GetIncomeCategoryResponse> pagedData;
    private IEnumerable<GetIncomeCategoryResponse> data;
    private MudTable<GetIncomeCategoryResponse> table;
    private int totalItems;
    private string searchString = null;

    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;
    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<GetIncomeCategoryResponse>> ServerReload(TableState state)
    {
        await LoadData(state.Page, state.PageSize);

        await SortAndPaged(state);

        return new TableData<GetIncomeCategoryResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData(int page, int pageSize)
    {
        list = await mediator.Send(new GetIncomeCategoryQuery(""));

        data = list.IncomeCategories.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (item.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "category":
                data = data.OrderByDirection(state.SortDirection, o => o.Name);
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
