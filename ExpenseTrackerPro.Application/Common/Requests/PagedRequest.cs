﻿namespace ExpenseTrackerPro.Application.Common.Requests;

public abstract class PagedRequest
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
