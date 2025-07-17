namespace Movies.Core;

public class PagingMeta(int totalItems, int currentPage, int pageSize)
{
    public int TotalItems { get; set; } = totalItems;
    public int CurrentPage { get; set; } = currentPage;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalItems / (double)pageSize);
    public int PageSize { get; set; } = pageSize;
}