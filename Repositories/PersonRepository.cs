using Granthology.Data;
using Granthology.Models;
using Microsoft.EntityFrameworkCore;

namespace Granthology.Repositories
{
    public class PersonRepository : Repository<Person>
    {

        public PersonRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override IEnumerable<Person> FindAll(int page = 0, int max = int.MaxValue, Func<Person, bool>? filter = null, Func<Person, dynamic>? order = null, bool orderAscending = true)
        {
            IEnumerable<Person> query = _context.People.Where(p => p.DeletedAt == null)
                                                            .Include(p => p.Relationships)
                                                                .ThenInclude(r => r.PersonA)
                                                            .Include(p => p.Relationships)
                                                                .ThenInclude(r => r.PersonB);

            if (filter != null) query = query.Where(filter);

            if (order != null)
            {
                if (orderAscending) query = query.OrderBy(order);
                else query = query.OrderByDescending(order);
            }


            return query.Skip(page * max).Take(max).ToList();

        }

        public override async Task<Person?> FindById(int id)
        {
            Person person = await _context.People
                                    .Include(p => p.Relationships)
                                        .ThenInclude(r => r.PersonA)
                                    .Include(p => p.Relationships)
                                        .ThenInclude(r => r.PersonB)
                                    .Where(p => p.Id == id)
                                    .FirstOrDefaultAsync();

            return person;
        }

        public override async Task<Person> Insert(Person entity)
        {
            return await base.Insert(entity);
        }

        public override async Task<bool> Update(Person oldEntity, Person newEntity)
        {
            oldEntity.FirstName = newEntity.FirstName;
            oldEntity.MiddleName = newEntity.MiddleName;
            oldEntity.LastName = newEntity.LastName;
            oldEntity.DateOfBirth = newEntity.DateOfBirth;
            oldEntity.DateOfDeath = newEntity.DateOfDeath;
            oldEntity.PlaceOfBirth = newEntity.PlaceOfBirth;
            oldEntity.PlaceOfDeath = newEntity.PlaceOfDeath;
            oldEntity.IsDeceased = newEntity.IsDeceased;
            oldEntity.Relationships = newEntity.Relationships;

            oldEntity.LastUpdatedAt = DateTime.Now;
            return await base.Update(oldEntity, newEntity);
        }

        public async Task<bool> Update(Person newEntity)
        {
            Person? oldEntity = await FindById(newEntity.Id);
            if (oldEntity == null) return false;
            return await Update(oldEntity, newEntity);
        }

    }
}
