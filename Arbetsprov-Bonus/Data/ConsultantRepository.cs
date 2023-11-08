using Arbetsprov_Bonus.Entities;

namespace Arbetsprov_Bonus.Data;

public class ConsultantRepository : IConsultantRepository
{
    private readonly GisysDbContext _gisysDbContext;

    public ConsultantRepository(GisysDbContext gisysDbContext)
    {
        _gisysDbContext = gisysDbContext;
    }

    public void Add(Consultant consultant)
    {
        _gisysDbContext.Consultants.Add(consultant);
        _gisysDbContext.SaveChanges();
    }

    public IEnumerable<Consultant> Get()
    {
        return _gisysDbContext.Consultants.ToList();
    }
    public Consultant? GetById(int id)
    {
        return _gisysDbContext.Consultants.FirstOrDefault(c => c.Id == id);
    }
    public void Remove(int id)
    {
        var consultant = _gisysDbContext.Consultants.Find(id);
        if (consultant != null)
        {
            _gisysDbContext.Consultants.Remove(consultant);
            _gisysDbContext.SaveChanges();
        }
    }
    public void Update(Consultant consultant)
    {
        // Assuming that consultant is already being tracked by the _gisysDbContext
        _gisysDbContext.SaveChanges();
    }

}
