using ApplicationDbContext;
using Interface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcess
{
    public class ThingDA : IThingDA
    {
        private readonly AppDbContext appDbContext;
        public ThingDA(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<List<Thing>> getAllThing()
        {
            return await appDbContext.Things.ToListAsync();
        }
        public async Task<Thing?> getThing(string codeThing)
        {
            return await appDbContext.Things.AsNoTracking().FirstOrDefaultAsync(u => u.Code == codeThing);
        }
        public async Task<int> addThing(Thing thing)
        {
            List<Thing>? lst = appDbContext.Things.Count() > 0 ? appDbContext.Things.ToList() : null;
            int maxid = 0;
            foreach (Thing temp in lst)
                if (temp.Id_thing > maxid)
                    maxid = temp.Id_thing;
            thing.Id_thing = maxid + 1;
            thing.Id_room = 1;
            appDbContext.Things.Add(thing);
            await appDbContext.SaveChangesAsync();
            return thing.Id_student;
        }
        public async Task<int> changeRoomThing(string codeThing, int id_room)
        {
            Thing thing = await appDbContext.Things.Where(t => t.Code == codeThing).FirstOrDefaultAsync();
            thing.Id_room = id_room;
            await appDbContext.SaveChangesAsync();
            return 1;
        }
        public async Task<int> changeStudentThing(string codeThing, int id_student)
        {
            Thing thing = await appDbContext.Things.Where(t => t.Code == codeThing).FirstOrDefaultAsync();
            thing.Id_student = id_student;
            await appDbContext.SaveChangesAsync();
            return 1;
        }
    }
}
