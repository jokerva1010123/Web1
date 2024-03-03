using Exceptions;
using Interface;
using Models;

namespace Services
{
	public class RoomServices
	{
		private readonly IRoomDA roomDA;

		public RoomServices(IRoomDA roomDA)
		{
			this.roomDA = roomDA;
		}
		public async Task<List<Room>> getAllRoom()
		{
			return await roomDA.getAllRoom();
		}
		public async Task<Room> getRoomById(int id_room)
		{
			Room? room = await roomDA.getRoomById(id_room);
			if (room == null)
				throw new RoomNotFoundException();
			return room;
		}
	}
}
