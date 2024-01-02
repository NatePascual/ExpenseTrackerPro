using ExpenseTrackerPro.Application.Features.Institutions;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Institutions;

public partial class InstitutionOverview
{
    private GetInstitutionView? list;
    private IEnumerable<GetInstitutionResponse> pagedData;
    private IEnumerable<GetInstitutionResponse> data;
    private MudTable<GetInstitutionResponse> table;
    private int totalItems;
    private string searchString = null;

    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;
    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<GetInstitutionResponse>> ServerReload(TableState state)
    {
        await LoadData(state.Page, state.PageSize);

        await SortAndPaged(state);

        return new TableData<GetInstitutionResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData(int page, int pageSize)
    {
        list = await mediator.Send(new GetInstitutionQuery(searchString));

        data = list.Institutions.Data;

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
            case "name":
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
