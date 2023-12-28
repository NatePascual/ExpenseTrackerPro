using ExpenseTrackerPro.Application.Features.Categories;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Categories;

public partial class ManageCategory
{
    private GetCategoryView? list;
    private IEnumerable<GetCategoryResponse> pagedData;
    private IEnumerable<GetCategoryResponse> data;
    private MudTable<GetCategoryResponse> table;
    private int totalItems;
    private string searchString = null;

    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;

    private TableGroupDefinition<GetCategoryResponse> _groupDefinition = new()
    {
        GroupName = "Group",
        Indentation = false,
        Expandable = true,
        IsInitiallyExpanded = true,
        Selector = (e) => e.ParentName

    };
    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<GetCategoryResponse>> ServerReload(TableState state)
    {
        await LoadData(state.Page, state.PageSize);

        await SortAndPaged(state);

        return new TableData<GetCategoryResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData(int page, int pageSize)
    {
        list = await mediator.Send(new GetCategoryQuery(""));

        data = list.Categories.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if ((string.IsNullOrEmpty(item.ParentName) && (item.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))) ||
               (!string.IsNullOrEmpty(item.ParentName) && (item.ParentName.Contains(searchString, StringComparison.OrdinalIgnoreCase))))
            {
                return true;
            }

            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "parentCategory":
                data = data.OrderByDirection(state.SortDirection, o => o.ParentName);
                break;
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
