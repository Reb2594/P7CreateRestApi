using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface ICurvePointRepository
    {
        Task<List<CurvePoint>> GetAllAsync();
        Task<CurvePoint?> GetByIdAsync(int id);
        Task<CurvePoint> CreateAsync(CurvePoint curvePoint);
        Task<CurvePoint?> UpdateAsync(CurvePoint curvePoint);
        Task DeleteAsync(int id);
    }
}
