using ExpenseTrackerPro.Application.Features.Accounts;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Accounts;

public partial class ManageAccount
{
    private GetAccountView? list;
    private IEnumerable<GetAccountResponse> pagedData;
    private IEnumerable<GetAccountResponse> data;
    private MudTable<GetAccountResponse> table;
    private int totalItems;
    private string searchString = null;

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<GetAccountResponse>> ServerReload(TableState state)
    {
        await LoadData(state.Page, state.PageSize);

        await SortAndPaged(state);

        return new TableData<GetAccountResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData(int page, int pageSize)
    {
        list = await mediator.Send(new GetAccountQuery(searchString));

        data = list.Accounts.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (item.AccountTypeName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.InstitutionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.AccountNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "accountTypeName":
                data = data.OrderByDirection(state.SortDirection, o => o.AccountTypeName);
                break;
            case "institutionName":
                data = data.OrderByDirection(state.SortDirection, o => o.InstitutionName);
                break;
            case "accountName":
                data = data.OrderByDirection(state.SortDirection, o => o.AccountTypeName);
                break;
            case "accountNumber":
                data = data.OrderByDirection(state.SortDirection, o => o.AccountNumber);
                break;
            case "balance":
                data = data.OrderByDirection(state.SortDirection, o => o.Balance);
                break;
        }

        pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
    }
    private void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }

    private async Task InvokeModal(int id = 0)
    {
        var parameters = new DialogParameters();
        if (id != 0)
        {
            var product = pagedData.FirstOrDefault(c => c.Id == id);
            if (product != null)
            {
                parameters.Add(nameof(CreateUpdateAccount.CreateUpdateAccountModel), new CreateUpdateAccountCommand
                {
                    Id = product.Id,
                    Name = product.Name,
                });
            }
        }
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
        var dialog = _dialogService.Show<CreateUpdateAccount>(id == 0 ? "Create" : "Update", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            OnSearch("");
        }
    }
}
