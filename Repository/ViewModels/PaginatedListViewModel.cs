namespace Repository.ViewModels;

public class PaginatedListViewModel<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string SearchString { get; set; } = string.Empty;
}
