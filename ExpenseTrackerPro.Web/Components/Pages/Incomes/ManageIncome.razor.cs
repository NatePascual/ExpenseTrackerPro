using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.Incomes;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Incomes;

public partial class ManageIncome
{
    private GetIncomeView? list;
    private IEnumerable<GetIncomeResponse> pagedData;
    private IEnumerable<GetIncomeResponse> data;
    private MudTable<GetIncomeResponse> table;

    private int totalItems;
    private string searchString = null;
    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;

    private TableGroupDefinition<GetIncomeResponse> _groupDefinition = new()
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
    private async Task<TableData<GetIncomeResponse>> ServerReload(TableState state)
    {
        await LoadData();

        await SortAndPaged(state);

        return new TableData<GetIncomeResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData()
    {
        list = await _mediator.Send(new GetIncomeQuery(""));

        data = list.Incomes.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (item.AccountName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.IncomeCategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.Amount.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.TransactionDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "incomeCategoryName":
                data = data.OrderByDirection(state.SortDirection, o => o.IncomeCategoryName);
                break;
            case "accountName":
                data = data.OrderByDirection(state.SortDirection, o => o.AccountName);
                break;
            case "amount":
                data = data.OrderByDirection(state.SortDirection, o => o.Amount);
                break;
            default:
                data = data.OrderByDirection(SortDirection.Descending, o => o.TransactionDate);
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
            var income = pagedData.FirstOrDefault(c => c.Id == id);
            if (income != null)
            {
                parameters.Add(nameof(CreateUpdateIncome.CreateUpdateIncomeModel), new CreateUpdateIncomeCommand
                {
                    Id = income.Id,
                    IncomeCategoryId = income.IncomeCategoryId,
                    AccountId = income.AccountId,
                    Amount = income.Amount,
                    TransactionDate = income.TransactionDate,
                    Note = income.Note,
                    Photo = income.Photo,
                    IncomeCategoryImageUrl = income.IncomeCategoryImageUrl,
                    InstitutionImageUrl = income.InstitutionImageUrl,
                });
            }
        }
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            Position = DialogPosition.Center,
            FullWidth = true,
            DisableBackdropClick = true
        };
        var dialog = _dialogService.Show<CreateUpdateIncome>(id == 0 ? "Create" : "Update", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            OnSearch("");
        }
    }

    private async Task Delete(int id)
    {
        string deleteContent = "Do you really want to delete this Income? This process cannot be undone.";
        var parameters = new DialogParameters<Shared.Dialogs.DeleteConfirmation>();
        parameters.Add(x => x.ContentText, deleteContent);
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            Position = DialogPosition.Center,
            FullWidth = true,
            DisableBackdropClick = true
        };
        var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            //var response = await _mediator.Send(new DeleteIncomeCommand(id));
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
