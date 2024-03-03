using Models;

namespace Interface
{
    public interface IThingDA
    {
        public Task<List<Thing>> getAllThing();
        public Task<Thing?> getThing(string codeThing);
        public Task<int> addThing(Thing thing);
        public Task<int> changeRoomThing(string codeThing, int id_room);
        public Task<int> changeStudentThing(string codeThing, int id_student);
    }
}
