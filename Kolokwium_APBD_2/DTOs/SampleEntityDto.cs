namespace Kolokwium_APBD_2.DTOs;

// GET /api/sampleentity — lista, bez zagnieżdżeń
public class SampleEntityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// GET /api/sampleentity/{id} — szczegóły z zagnieżdżonymi powiązanymi obiektami
public class SampleEntityWithDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<RelatedItemDto> RelatedItems { get; set; } = [];
}

// zagnieżdżony obiekt w SampleEntityWithDetailsDto
public class RelatedItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    // jeśli jest kolejny poziom zagnieżdżenia (np. manufacturer w component):
    public NestedRelatedItemDto? NestedItem { get; set; }
}

// kolejny poziom zagnieżdżenia (np. typ/producent powiązanego obiektu)
public class NestedRelatedItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// POST /api/sampleentity — tworzenie nowego rekordu
public class AddSampleEntityDto
{
    public string Name { get; set; } = string.Empty;
    // public int RelatedEntityId { get; set; }  // FK jeśli tworzysz z relacją
}
