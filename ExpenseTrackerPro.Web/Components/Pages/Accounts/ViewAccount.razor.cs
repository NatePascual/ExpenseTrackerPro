using ExpenseTrackerPro.Application.Features.Accounts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Accounts;

public partial class ViewAccount
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public int Id { get; set; }
    [Parameter] public GetAccountByIdResponse GetAccountByIdResponseModel { get; set; } = new();
    public string TransactionDate {  get; set; }
    public string Description { get; set; }
    private int ImageHeight { get; } = 70;
    private int ImageWidth { get; } = 70;

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        var list = await _mediator.Send(new GetAccountByIdQuery(Id));

        GetAccountByIdResponseModel = list.Account.Data;

        if (GetAccountByIdResponseModel != null)
        {
            Description = $"{GetAccountByIdResponseModel.AccountTypeName} | {GetAccountByIdResponseModel.Name} | {GetAccountByIdResponseModel.AccountNumber}";
            if(GetAccountByIdResponseModel.LastModified!=null)
            {
                TransactionDate = $"Last updated {GetAccountByIdResponseModel.LastModified.Value.ToString("f")}";
            }
        }
    }
}
