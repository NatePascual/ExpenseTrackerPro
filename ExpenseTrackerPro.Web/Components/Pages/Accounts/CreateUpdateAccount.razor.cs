using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.AccountTypes;
using ExpenseTrackerPro.Application.Features.Currencies;
using ExpenseTrackerPro.Application.Features.Institutions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Accounts;

public partial class CreateUpdateAccount
{
    [Parameter] public CreateUpdateAccountCommand CreateUpdateAccountModel { get; set; } = new();
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    private List<GetAccountTypeResponse> _accountTypes = new();
    private List<GetInstitutionResponse> _institutions = new();
    private List<GetCurrencyResponse> _currencies = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        await LoadAccountTypes();
        await LoadInstitutions();
        await LoadCurrencies();
    }

    private async Task LoadAccountTypes()
    {
        var list = await _mediator.Send(new GetAccountTypeQuery(""));

        if (list.AccountTypes.Succeeded)
        {
            _accountTypes = list.AccountTypes.Data.ToList();
        }
    }

    private async Task<IEnumerable<int>> SearchAccountType(string search)
    {      
        if (string.IsNullOrEmpty(search))
            return _accountTypes.Select(x => x.Id);

        return _accountTypes.Where(x => x.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Id);
    }

    private async Task LoadInstitutions()
    {
        var list = await _mediator.Send(new GetInstitutionQuery(""));

        if (list.Institutions.Succeeded)
        {
            _institutions = list.Institutions.Data.ToList();
        }
    }

    private async Task<IEnumerable<int>> SearchInstitution(string search)
    {
        if (string.IsNullOrEmpty(search))
            return _institutions.Select(x => x.Id);

        return _institutions.Where(x => x.Name.Contains(search,StringComparison.InvariantCultureIgnoreCase)).Select(x =>x.Id);
    }

    private async Task LoadCurrencies()
    {
        var list = await _mediator.Send(new GetCurrencyQuery(""));

        if (list.Currencies.Succeeded)
        {
            _currencies = list.Currencies.Data.ToList();
        }
    }
    private async Task<IEnumerable<int>> SearchCurrency(string search)
    {
        if (string.IsNullOrEmpty(search))
            return _currencies.Select(x => x.Id);

        return _currencies.Where(x => x.Code.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Id);
    }

    private async Task SaveAsync()
    {
        var response = await _mediator.Send(CreateUpdateAccountModel);
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
