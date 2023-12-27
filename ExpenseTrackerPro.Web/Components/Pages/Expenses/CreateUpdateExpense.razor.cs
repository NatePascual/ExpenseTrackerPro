using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.Categories;
using ExpenseTrackerPro.Application.Features.Expenses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Expenses;

public partial class CreateUpdateExpense
{
    [Parameter] public CreateUpdateExpenseCommand CreateUpdateExpenseModel { get; set; } = new();
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    private List<GetCategoryResponse> _categories { get; set; } = new();
    private List<GetAccountResponse> _accounts { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        await LoadCategory();
        await LoadAccounts();
    }

    private async Task LoadCategory()
    {
        var list = await _mediator.Send(new GetCategoryQuery(""));

        if (list.Categories.Succeeded)
        {
            _categories = list.Categories.Data.ToList();
        }
    }

    private async Task<IEnumerable<int>> SearchIncomeCategory(string search)
    {
        if (string.IsNullOrEmpty(search))
            return _categories.Select(x => x.Id);

        return _categories.Where(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Id);
    }
    private async Task LoadAccounts()
    {
        var list = await _mediator.Send(new GetAccountQuery(""));

        if (list.Accounts.Succeeded)
        {
            _accounts = list.Accounts.Data.ToList();
        }
    }

    private async Task<IEnumerable<int>> SearchAccount(string search)
    {
        if (string.IsNullOrEmpty(search))
            return _accounts.Select(x => x.Id);

        return _accounts.Where(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Id);
    }

    private async Task SaveAsync()
    {
        var response = await _mediator.Send(CreateUpdateExpenseModel);
        if (response.Succeeded)
        {
            _snackBar.Add(response.Messages[0], Severity.Success);
            MudDialog.Close();
        }
        else
        {
            foreach (var message in response.Messages)
            {
                _snackBar.Add(message, Severity.Error);
            }
        }
    }
    public void Cancel()
    {
        MudDialog.Cancel();
    }
}
