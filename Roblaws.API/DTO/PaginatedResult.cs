namespace JWTAuthTemplate.DTO;

public class PaginatedResult<T> {
    public T Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}