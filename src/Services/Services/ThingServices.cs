using Interface;
using Exceptions;
using Models;

namespace Services
{
    public class ThingServices
    {
        protected IThingDA thingDA;
        protected IStudentDA studentDA;
        protected IRoomDA roomDA;
        public ThingServices(IThingDA thingDA, IStudentDA studentDA, IRoomDA roomDA)
        {
            this.thingDA = thingDA;
            this.studentDA = studentDA;
            this.roomDA = roomDA;
        }
        public async Task<List<Thing>> getAllThing()
        {
            return await thingDA.getAllThing();
        }
        public async Task<Thing> getThing(string codeThing)
        {
            if (string.IsNullOrEmpty(codeThing))
                throw new InputInvalidException();
            Thing? thing = await thingDA.getThing(codeThing);
            if (thing == null)
                throw new ThingNotFoundException();
            return thing;
        }
        public async Task<int> addThing(Thing thing)
        {
            if (string.IsNullOrEmpty(thing.Code) || string.IsNullOrEmpty(thing.Type))
                throw new InputInvalidException();
            Thing? temp = await thingDA.getThing(thing.Code);
            if (temp != null)
                throw new ThingCodeExistsException();
            return await thingDA.addThing(thing);
        }
        public async Task<int> changeRoomThing(string codeThing, int id_room)
        {
            Thing? thing = await thingDA.getThing(codeThing);
            if (thing == null)
                throw new ThingNotFoundException();
            Room? room = await roomDA.getRoomById(id_room);
            if (room == null)
                throw new RoomNotFoundException();
            if (thing.Id_room == id_room)
                throw new ThingInRoomException();
            await thingDA.changeRoomThing(codeThing, id_room);
            return 1;
        }
        public async Task<int> changeStudentThing(string codeThing, int id_student)
        {
            Thing? thing = await thingDA.getThing(codeThing);
            if (thing == null)
                throw new ThingNotFoundException();
            if (id_student > 0)
            {
                Student? student = await studentDA.getStudentById(id_student);
                if (student == null)
                    throw new StudentNotFoundException();
                if (thing.Id_student > 0)
                    throw new ThingNotFreeException();
                await thingDA.changeStudentThing(codeThing, id_student);
                return 1;
            }
            if (thing.Id_student == 0)
                throw new ThingFreeException();
            await thingDA.changeStudentThing(codeThing, id_student);
            return 1;
        }
    }
}
