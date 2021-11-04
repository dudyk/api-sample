using System;

namespace CarryLoad.Models.Entities.Interfaces
{
    public interface IDeletableEntity<TKey> : IEntity<TKey>
    {
        DateTime? DeletedAt { get; set; }
    }
}