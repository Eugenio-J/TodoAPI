using TodoAPI.DTOs;

namespace TodoAPI.Services
{
	public interface ITodoService
	{
		Task<GetTask> AddTask(CreateTask request);
		Task<int> UpdateStatus(int taskId);
		Task<int> UpdateTask(CreateTask request, int taskId);
		Task<int> RemoveTask(int taskId);
		Task<List<GetTask>> GetAllTask();
		Task<GetTask?> GetSingleTask(int Id);
	}
}
