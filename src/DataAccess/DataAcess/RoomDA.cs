using ApplicationDbContext;
using Models;
using Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAcess
{
	public class RoomDA : IRoomDA
	{
		private readonly AppDbContext appContext;
		public RoomDA(AppDbContext appContext)
		{
			this.appContext = appContext;
		}
		public async Task<List<Room>> getAllRoom()
		{
			return await appContext.Rooms.ToListAsync();
		}
		public async Task<Room?> getRoomById(int id_room)
		{
			return await appContext.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.Id_room == id_room);
		}
	}
}
