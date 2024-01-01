using ExpenseTrackerPro.Application.Features.Transfers;
using MudBlazor;

namespace ExpenseTrackerPro.Web.Components.Pages.Transfers;

public partial class ManageTransfer
{
    private GetTransferView? list;
    private IEnumerable<GetTransferResponse> pagedData;
    private IEnumerable<GetTransferResponse> data;
    private MudTable<GetTransferResponse> table;
    private int totalItems;
    private string searchString = null;
    private int ImageHeight { get; } = 50;
    private int ImageWidth { get; } = 50;

    private TableGroupDefinition<GetTransferResponse> _groupDefinition = new()
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
    private async Task<TableData<GetTransferResponse>> ServerReload(TableState state)
    {
        await LoadData(state.Page, state.PageSize);

        await SortAndPaged(state);

        return new TableData<GetTransferResponse>() { TotalItems = totalItems, Items = pagedData };
    }

    private async Task LoadData(int page, int pageSize)
    {
        list = await _mediator.Send(new GetTransferQuery(""));

        data = list.Transfers.Data;

        data = data.Where(item =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (item.SenderName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                item.ReceiverName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                item.Note.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (item.Amount.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }).ToArray();
        totalItems = data.Count();
    }

    private async Task SortAndPaged(TableState state)
    {
        switch (state.SortLabel)
        {
            case "senderName":
                data = data.OrderByDirection(state.SortDirection, o => o.SenderName);
                break;
            case "receiverName":
                data = data.OrderByDirection(state.SortDirection, o => o.ReceiverName);
                break;
            case "amout":
                data = data.OrderByDirection(state.SortDirection, o => o.Amount);
                break;
            case "note":
                data = data.OrderByDirection(state.SortDirection, o => o.Note);
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
            var transfer = pagedData.FirstOrDefault(c => c.Id == id);
            if (transfer != null)
            {
                parameters.Add(nameof(CreateUpdateTransfer.CreateUpdateTransferModel), new CreateUpdateTransferCommand
                {
                    Id = transfer.Id,
                    SenderId = transfer.SenderId,
                    SenderInstitutionImageUrl = transfer.SenderInstitutionImageUrl,
                    ReceiverId = transfer.ReceiverId,
                    ReceiverInstitutionImageUrl = transfer.ReceiverInstitutionImageUrl,
                    TransactionDate = transfer.TransactionDate,
                    Amount = transfer.Amount,
                    Note = transfer.Note
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
        var dialog = _dialogService.Show<CreateUpdateTransfer>(id == 0 ? "Create" : "Update", parameters, options);
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
