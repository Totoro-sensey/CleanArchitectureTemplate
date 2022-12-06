using Domain.Common;

namespace Domain.Entities;

public class Office : BaseEntity
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
}