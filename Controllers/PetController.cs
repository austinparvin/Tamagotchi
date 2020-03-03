using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Models;

namespace Tamagotchi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        public DatabaseContext db { get; set; } = new DatabaseContext();
        public Random r { get; set; } = new Random();

        // Get All Pets
        [HttpGet("all")]
        public List<Pet> GetAllPets()
        {
            var allPets = db.Pets.ToList();
            foreach (Pet p in allPets)
            {

                if (((TimeSpan)(DateTime.Now - p.LastInteractedWithDate)).TotalDays >= 3)
                {
                    p.IsDead = true;
                    p.DeathDate = ((DateTime)p.LastInteractedWithDate).AddDays(3);
                }
                p.LastInteractedWithDate = DateTime.Now;

            }
            db.SaveChanges();
            return allPets;
        }
        [HttpGet("all/alive")]
        public List<Pet> GetAllAlivePets()
        {
            var allPets = db.Pets.ToList();
            foreach (Pet p in allPets)
            {

                if (((TimeSpan)(DateTime.Now - p.LastInteractedWithDate)).TotalDays >= 3)
                {
                    p.DeathDate = ((DateTime)p.LastInteractedWithDate).AddDays(3);
                    p.IsDead = true;
                }
                p.LastInteractedWithDate = DateTime.Now;

            }
            db.SaveChanges();
            var allAlivePets = db.Pets.Where(p => p.IsDead == false).ToList();

            return allAlivePets;
        }

        // Get Pet By Id
        [HttpGet("{id}")]
        public Pet GetAPet(int id)
        {
            var pet = db.Pets.FirstOrDefault(p => p.Id == id);
            if (((TimeSpan)(DateTime.Now - pet.LastInteractedWithDate)).TotalDays >= 3)
            {
                pet.DeathDate = ((DateTime)pet.LastInteractedWithDate).AddDays(3);
                pet.IsDead = true;
            }
            pet.LastInteractedWithDate = DateTime.Now;
            db.SaveChanges();
            return pet;
        }

        // Create Pet
        [HttpPost]
        public ActionResult CreatePet(Pet petToAdd)
        {
            db.Pets.Add(petToAdd);
            db.SaveChanges();
            return Ok();
        }

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

        [HttpPut("{id}/feed")]
        public Pet Feed(int id)
        {

            var petToFeed = db.Pets.FirstOrDefault(p => p.Id == id);
            if (r.Next(0, 9) == 0 && petToFeed.IsDead == false)
            {
                petToFeed.DeathDate = DateTime.Now;
                petToFeed.IsDead = true;
            }
            if (((TimeSpan)(DateTime.Now - petToFeed.LastInteractedWithDate)).TotalDays >= 3)
            {
                petToFeed.DeathDate = ((DateTime)petToFeed.LastInteractedWithDate).AddDays(3);
                petToFeed.IsDead = true;
            }
            petToFeed.LastInteractedWithDate = DateTime.Now;
            petToFeed.HappinessLevel += 3;
            petToFeed.HungerLevel -= 5;
            db.SaveChanges();
            return petToFeed;

        }

        [HttpPut("{id}/scold")]
        public Pet Scold(int id)
        {
            var petToScold = db.Pets.FirstOrDefault(p => p.Id == id);
            if (r.Next(0, 9) == 0 && petToScold.IsDead == false)
            {
                petToScold.DeathDate = DateTime.Now;
                petToScold.IsDead = true;
            }
            if (((TimeSpan)(DateTime.Now - petToScold.LastInteractedWithDate)).TotalDays >= 3)
            {
                petToScold.DeathDate = ((DateTime)petToScold.LastInteractedWithDate).AddDays(3);
                petToScold.IsDead = true;
            }
            petToScold.LastInteractedWithDate = DateTime.Now;
            petToScold.HappinessLevel -= 5;
            db.SaveChanges();
            return petToScold;

        }

        [HttpDelete("{id}")]
        public ActionResult DeletePet(int id)
        {
            var petToDelete = db.Pets.FirstOrDefault(p => p.Id == id);
            db.Pets.Remove(petToDelete);
            db.SaveChanges();
            return Ok();
        }
    }
}