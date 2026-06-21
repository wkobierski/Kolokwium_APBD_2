# Code-First EF Core — Poradnik

---

## 1. Uruchom SQL Server w Dockerze

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=yourStrong(!)Password' \
  -p 1433:1433 --name mssql --hostname mssql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Sprawdź:
```bash
docker ps
```
Musi być `mssql` ze statusem `Up`.

> Jeśli kontener już istnieje (błąd "name already in use"):
> ```bash
> docker start mssql
> ```

---

## 2. Skopiuj szablon i zmień nazwę projektu

```bash
cp -r CodeFirst/ NazwaProjektu
cd NazwaProjektu
mv CodeFirst.csproj NazwaProjektu.csproj
```

Zrób **find & replace** w całym folderze: `CodeFirst` → `NazwaProjektu`

W VS Code: `Ctrl+Shift+H` → wyszukaj `CodeFirst`, zastąp `NazwaProjektu`, kliknij "Replace All".

---

## 3. Zmień port w `Properties/launchSettings.json`

Zmień `5050` na port podany w zadaniu (np. `5027`):

```json
"applicationUrl": "http://localhost:5027"
```

---

## 4. Zmień nazwę bazy w `appsettings.json`

Znajdź `ZMIEN_NAZWE_BAZY` i wstaw nazwę bazy z zadania:

```json
"Initial Catalog=nazwabazy;"
```

---

## 5. Zmień Entities

Otwórz `Entities/SampleEntity.cs`. Zmień nazwę klasy i właściwości według ERD z zadania.

**Wzorzec — zwykła encja:**
```csharp
// Entities/Animal.cs
namespace NazwaProjektu.Entities;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int CategoryId { get; set; }               // FK
    public Category Category { get; set; } = null!;   // nawigacja - jak jest rozgalezienie
    public ICollection<Visit> Visits { get; set; } = []; // relacja 1:N - jak jest krzyzyk, prosta linia
}
```

**Wzorzec — tabela łącząca (many-to-many z dodatkowym polem):**
```csharp
// Entities/AnimalVisit.cs
public class AnimalVisit
{
    public int AnimalId { get; set; }
    public int VisitId { get; set; }
    public int Amount { get; set; }  // dodatkowe pole

    public Animal Animal { get; set; } = null!;
    public Visit Visit { get; set; } = null!;
}
```

Dla każdej encji z zadania — nowy plik w `Entities/`.

---

## 6. Zmień Configurations

Otwórz `Configurations/SampleEntityConfiguration.cs`. Skopiuj plik dla każdej encji i dostosuj.

**Wzorzec:**
```csharp
public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.ToTable("Animals");

        // Relacja many-to-one (FK w tej tabeli):
        builder.HasOne(e => e.Category)
               .WithMany(c => c.Animals)
               .HasForeignKey(e => e.CategoryId);

        // Seed — min. 3 rekordy:
        builder.HasData(new List<Animal>
        {
            new() { Id = 1, Name = "Rex", Weight = 30.0, CategoryId = 1 },
            new() { Id = 2, Name = "Mruczek", Weight = 5.0, CategoryId = 2 },
            new() { Id = 3, Name = "Basia", Weight = 12.0, CategoryId = 1 },
        });
    }
}
```

**Tabela łącząca — klucz kompozytowy:**
```csharp
builder.HasKey(e => new { e.AnimalId, e.VisitId });

builder.HasOne(e => e.Animal)
       .WithMany(a => a.AnimalVisits)
       .HasForeignKey(e => e.AnimalId)
       .OnDelete(DeleteBehavior.Cascade);

builder.HasOne(e => e.Visit)
       .WithMany(v => v.AnimalVisits)
       .HasForeignKey(e => e.VisitId)
       .OnDelete(DeleteBehavior.NoAction);
```

---

## 7. Zaktualizuj `Data/AppDbContext.cs`

Dodaj `DbSet` dla każdej encji z zadania (zamień / dopisz):

```csharp
public DbSet<Animal> Animals { get; set; }
public DbSet<Category> Categories { get; set; }
public DbSet<Visit> Visits { get; set; }
public DbSet<AnimalVisit> AnimalVisits { get; set; }
```

Reszta pliku — nie ruszaj.

---

## 8. Zmień DTOs

Otwórz `DTOs/SampleEntityDto.cs`. Dostosuj właściwości do encji z zadania.

```csharp
// DTOs/AnimalDto.cs
public class AnimalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
}

public class AddAnimalDto
{
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int CategoryId { get; set; }
}

public class UpdateAnimalDto
{
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
}
```

**Jeśli endpoint zwraca zagnieżdżone dane (np. GET with details):**
```csharp
public class AnimalWithVisitsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<VisitDto> Visits { get; set; } = [];
}

public class VisitDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
}
```

---

## 9. Zaktualizuj `Services/IDbService.cs`

Zmień typy na swoje DTO:

```csharp
public interface IDbService
{
    Task<IEnumerable<AnimalDto>> GetAllAsync();
    Task<AnimalWithVisitsDto> GetByIdAsync(int id);  // jeśli endpoint zwraca szczegóły
    Task<AnimalDto> AddAsync(AddAnimalDto dto);
    Task UpdateAsync(int id, UpdateAnimalDto dto);
    Task DeleteAsync(int id);
}
```

---

## 10. Zaktualizuj `Services/DbService.cs`

Zmień `SampleEntity`/`SampleEntities` na swoje encje. Dostosuj projekcje `Select()`.

**GetAll — prosta lista:**
```csharp
public async Task<IEnumerable<AnimalDto>> GetAllAsync()
{
    return await _db.Animals
        .Select(e => new AnimalDto { Id = e.Id, Name = e.Name, Weight = e.Weight })
        .ToListAsync();
}
```

**GetById z Include (jeśli potrzeba relacji):**
```csharp
public async Task<AnimalWithVisitsDto> GetByIdAsync(int id)
{
    var item = await _db.Animals
        .Where(e => e.Id == id)
        .Select(e => new AnimalWithVisitsDto
        {
            Id = e.Id,
            Name = e.Name,
            Visits = e.Visits.Select(v => new VisitDto { Id = v.Id, Date = v.Date })
        })
        .FirstOrDefaultAsync();

    if (item == null) throw new NotFoundException($"Nie znaleziono id={id}");
    return item;
}
```

**Add:**
```csharp
public async Task<AnimalDto> AddAsync(AddAnimalDto dto)
{
    var item = new Animal { Name = dto.Name, Weight = dto.Weight, CategoryId = dto.CategoryId };
    await _db.AddAsync(item);
    await _db.SaveChangesAsync();
    return new AnimalDto { Id = item.Id, Name = item.Name, Weight = item.Weight };
}
```

---

## 11. Zaktualizuj `Controllers/SampleEntityController.cs`

Zmień nazwę klasy, route i typy DTO. Wzorzec kontrolera jest gotowy — podmień tylko nazwy.

```csharp
[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase   // nazwa klasy = nazwa route
{
    // reszta bez zmian, tylko typy DTO
}
```

---

## 12. Zainstaluj narzędzie EF (tylko raz, jeśli nie masz)

```bash
dotnet tool install --global dotnet-ef
```

---

## 13. Migracja i baza danych

```bash
dotnet ef migrations add Init
dotnet ef database update
```

Jeśli błąd migracji zrób nową na początku dotnet ef migrations remove i potem nowa— sprawdź czy `DbSet` w `AppDbContext` zgadza się z encjami w `Configurations/`.

---

## 14. Uruchom i sprawdź

```bash
dotnet run
```

Wejdź na `http://localhost:PORT/swagger` — wszystkie endpointy muszą być widoczne i działać.

---

## Kolejność pracy z templatem (ściągawka)

```
1. docker start mssql  (lub docker run jeśli pierwszy raz)
2. cp -r CodeFirst/ NazwaProjektu  →  rename + find&replace
3. launchSettings.json  →  zmień port
4. appsettings.json     →  zmień nazwę bazy
5. Entities/            →  nowe klasy według ERD
6. Configurations/      →  jeden plik na encję, seed min. 3 rekordy
7. AppDbContext.cs       →  dodaj DbSet dla każdej encji
8. DTOs/                →  dostosuj właściwości
9. IDbService.cs        →  zmień typy
10. DbService.cs        →  dostosuj Select() i mapowania
11. Controller          →  zmień nazwę i typy DTO
12. dotnet ef migrations add Init
13. dotnet ef database update
14. dotnet run  →  sprawdź Swagger
```

---

## Ściągawka statusów HTTP

| Sytuacja | Status |
|----------|--------|
| GET — znaleziono | 200 OK |
| POST — utworzono | 201 Created |
| PUT/DELETE — sukces | 204 No Content |
| Nie znaleziono zasobu | 404 Not Found |
| Błąd walidacji | 400 Bad Request |
| Błąd serwera | 500 Internal Server Error |