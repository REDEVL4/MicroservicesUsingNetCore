
namespace Discount.Api.DataAccess
{
    public interface IGenericRepository
    {
        Task InsertData<T, U>(T? data = default, U? parameters = default, string? query = default);
        Task<IEnumerable<T>> LoadData<T, U>(T? data = default, U? parameters = default, string? query = default);
    }
}