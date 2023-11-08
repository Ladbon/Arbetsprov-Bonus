using Arbetsprov_Bonus.Entities;

namespace Arbetsprov_Bonus.Data;

public interface IConsultantRepository
{
    /// <summary>
    /// Retrieves all consultants from the data store.
    /// </summary>
    /// <returns>A collection of all consultants.</returns>
    IEnumerable<Consultant> Get();

    /// <summary>
    /// Retrieves a single consultant by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the consultant to retrieve.</param>
    /// <returns>The consultant if found; otherwise, null.</returns>
    Consultant? GetById(int id);

    /// <summary>
    /// Removes a consultant from the data store by their ID.
    /// </summary>
    /// <param name="id">The ID of the consultant to remove.</param>
    void Remove(int id);

    /// <summary>
    /// Adds a new consultant to the data store.
    /// </summary>
    /// <param name="consultant">The consultant object to add.</param>
    void Add(Consultant consultant);

    /// <summary>
    /// Updates an existing consultant's information in the data store.
    /// </summary>
    /// <param name="consultant">The consultant object with updated information.</param>
    void Update(Consultant consultant);
}
