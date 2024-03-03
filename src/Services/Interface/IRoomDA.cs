using Models;

namespace Interface
{
	public interface IRoomDA
	{
		public Task<List<Room>> getAllRoom();
		public Task<Room?> getRoomById(int id_room);
	}
}
