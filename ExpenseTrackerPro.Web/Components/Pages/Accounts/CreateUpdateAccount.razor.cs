using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.AccountTypes;
using ExpenseTrackerPro.Application.Features.Currencies;
using ExpenseTrackerPro.Application.Features.Institutions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
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
    }

    public void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task LoadDataAsync()
    {
        await LoadAccountTypes();
        await LoadInstitutions();
        await LoadCurrencies();
    }

    private async Task LoadAccountTypes()
    {
        var list = await mediator.Send(new GetAccountTypeQuery(""));

        if (list.AccountTypes.Succeeded)
        {
            _accountTypes = list.AccountTypes.Data.ToList();
        }
    }

    private async Task LoadInstitutions()
    {
        var list = await mediator.Send(new GetInstitutionQuery(""));

        if (list.Institutions.Succeeded)
        {
            _institutions = list.Institutions.Data.ToList();
        }
    }

    private async Task LoadCurrencies()
    {
        var list = await mediator.Send(new GetCurrencyQuery(""));

        if (list.Currencies.Succeeded)
        {
            _currencies = list.Currencies.Data.ToList();
        }
    }
}
