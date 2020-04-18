# Objectives

- Create an API that can CRUD against a Database
- Reenforce SQL basics
- One to many relationships

# Includes: 

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [EF CORE](https://docs.microsoft.com/en-us/ef/core/)
- [POSTGRESQL](https://www.postgresql.org/)
- [CONTROLLERS](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.controller?view=aspnet-mvc-5.2)
- [POSTMAN](https://www.postman.com/)
- MVC design pattern

## Featured Code

### Api endpoint

```C#
  // "Play" With Pet
  [HttpPut("{id}/play")]
  public Pet Play(int id)
  {

      var petToPlayWith = db.Pets.FirstOrDefault(p => p.Id == id);
      if (r.Next(0, 9) == 0 && petToPlayWith.IsDead == false)
      {
          petToPlayWith.DeathDate = DateTime.Now;
          petToPlayWith.IsDead = true;
      }
      if (((TimeSpan)(DateTime.Now - petToPlayWith.LastInteractedWithDate)).TotalDays >= 3)
      {
          petToPlayWith.DeathDate = ((DateTime)petToPlayWith.LastInteractedWithDate).AddDays(3);
          petToPlayWith.IsDead = true;
      }
      petToPlayWith.LastInteractedWithDate = DateTime.Now;
      petToPlayWith.HappinessLevel += 5;
      petToPlayWith.HungerLevel += 3;
      db.SaveChanges();
      return petToPlayWith;

        }
 ```
 
## User Actions

- `GET /api/pets`, this returns all pets in the database
- `GET /api/pets/{id}`, This retursn the pet with the corresponding Id.
- `POST /api/pets`, This creates a new pet. The body of the request should contain the name of the pet.
- `PUT /api/pets/{id}/play`, This finds the pet by id, and add 5 to its happiness level and add 3 to its hungry level
- `PUT /api/pets/{id}/feed`, This finds the pet by id, and remove 5 from its hungry level and add 3 to its happiness level.
- `PUT /api/pets/{id}/scold`, This finds the pet by id, and remove 5 from its happiness level
- `DELETE /api/pets/{id}`, This deletes a pet from the database by Id

## App In Action

(need link to deployed api)
