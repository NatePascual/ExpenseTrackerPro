using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.IncomeCategories;
using ExpenseTrackerPro.Application.Features.Incomes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Incomes;

public partial class CreateUpdateIncome
{
    [Parameter] public CreateUpdateIncomeCommand CreateUpdateIncomeModel { get; set; } = new();
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    private List<GetIncomeCategoryResponse> _incomeCategories { get; set; } = new();
    private List<GetAccountResponse> _accounts { get; set; } = new();

    private bool _isDisabled { get;  set; } = false;
    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;

    private int _mediumSetting = 12;
    private int _smallSetting = 12;
    protected override async Task OnInitializedAsync()
    {
        if (CreateUpdateIncomeModel.Id != 0)
        {
            _mediumSetting = 10;
            _smallSetting = 10;
            _isDisabled = true;
        }

        await LoadDataAsync();
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        await LoadIncomeCategory();
        await LoadAccounts();
    }

    private async Task LoadIncomeCategory()
    {
        var list = await _mediator.Send(new GetIncomeCategoryQuery(""));

        if (list.IncomeCategories.Succeeded)
        {
            _incomeCategories = list.IncomeCategories.Data.ToList();
        }
    }

    private async Task<IEnumerable<int>> SearchIncomeCategory(string search)
    {
        if (string.IsNullOrEmpty(search))
             return _incomeCategories.Select(x => x.Id);

        return _incomeCategories.Where(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Id);
    }
    private async Task LoadAccounts()
    {
        var list = await _mediator.Send(new GetAccountQuery(""));

        if(list.Accounts.Succeeded)
        {
            _accounts = list.Accounts.Data.ToList();
        }
    }

    private async Task<IEnumerable<int>> SearchAccount(string search)
    {
        if(string.IsNullOrEmpty(search))
            return _accounts.Select(x => x.Id);

        return _accounts.Where(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Id);
    }

    private string ReturnAccount(int id)
    {
        var entity = _accounts.FirstOrDefault(x => x.Id == id);
        var result = string.Empty;
        if (entity != null)
        {
            result = $"( {entity.InstitutionName} ) {entity.AccountTypeName}  | {entity.Name} | {entity.CurrencySymbol} {entity.Balance.ToString("#,###.00")}";
        }

        return result;
    }

    private async Task SaveAsync()
    {
        var response = await _mediator.Send(CreateUpdateIncomeModel);
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
