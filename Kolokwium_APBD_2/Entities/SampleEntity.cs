namespace Kolokwium_APBD_2.Entities;

public class SampleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Nawigacja (jeśli jest relacja one-to-many):
    // public ICollection<RelatedEntity> RelatedEntities { get; set; } = [];
}

// ===== WZORZEC: tabela łącząca (many-to-many z polem dodatkowym) =====
// public class SampleJunction
// {
//     public int SampleEntityId { get; set; }
//     public int OtherEntityId { get; set; }
//     public int Amount { get; set; }   // dodatkowe pole
//
//     public SampleEntity SampleEntity { get; set; } = null!;
//     public OtherEntity OtherEntity { get; set; } = null!;
// }
