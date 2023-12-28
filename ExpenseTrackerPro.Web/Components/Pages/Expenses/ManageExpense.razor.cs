using ExpenseTrackerPro.Application.Features.Expenses;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Expenses;

public partial class ManageExpense
{
    private GetExpenseView? list;
    private IEnumerable<GetExpenseResponse> pagedData;
    private IEnumerable<GetExpenseResponse> data;
    private MudTable<GetExpenseResponse> table;
    private int totalItems;
    private string searchString = null;
    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;

    private TableGroupDefinition<GetExpenseResponse> _groupDefinition = new()
    {
        GroupName = "Group",
        Indentation = false,
        Expandable = true,
        IsInitiallyExpanded = true,
        Selector = (e) => e.TransactionDate.Value.ToString("d")

    };
    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<GetExpenseResponse>> ServerReload(TableState state)
    {
        await LoadData(state.Page, state.PageSize);

        await SortAndPaged(state);

        return new TableData<GetExpenseResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData(int page, int pageSize)
    {
        list = await _mediator.Send(new GetExpenseQuery(searchString));

        data = list.Expenses.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (item.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.AccountName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.Provider.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
           
            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "categoryName":
                data = data.OrderByDirection(state.SortDirection, o => o.CategoryName);
                break;
            case "accountName":
                data = data.OrderByDirection(state.SortDirection, o => o.AccountName);
                break;
            case "provider":
                data = data.OrderByDirection(state.SortDirection, o => o.Provider);
                break;
            case "amount":
                data = data.OrderByDirection(state.SortDirection, o => o.Amount);
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
            var expense = pagedData.FirstOrDefault(c => c.Id == id);
            if (expense != null)
            {
                parameters.Add(nameof(CreateUpdateExpense.CreateUpdateExpenseModel), new CreateUpdateExpenseCommand
                {
                    Id = expense.Id,
                    CategoryId = expense.CategoryId,
                    AccountId = expense.AccountId,
                    Provider = expense.Provider,
                    TransactionDate = expense.TransactionDate,
                    Amount = expense.Amount,
                    Note = expense.Note,
                    Photo = expense.Photo,
                    InstitutionImageUrl = expense.InstitutionImageUrl,
                    CategoryImageUrl = expense.CategoryImageUrl
                });
            }
        }
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            Position = DialogPosition.TopLeft,
            FullWidth = true,
            DisableBackdropClick = true
        };
        var dialog = _dialogService.Show<CreateUpdateExpense>(id == 0 ? "Create" : "Update", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            OnSearch("");
        }
    }

    private async Task Delete(int id)
    {
        string deleteContent = "Delete Content";
        var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
        var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            //DeleteAsync(id)
            //var response = await _mediator.Send();
            //if (response.Succeeded)
            //{
            //    OnSearch("");
            //    _snackBar.Add(response.Messages[0], Severity.Success);
            //}
            //else
            //{
            //    OnSearch("");
            //    foreach (var message in response.Messages)
            //    {
            //        _snackBar.Add(message, Severity.Error);
            //    }
            //}
        }
    }
}
