using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services
{
	public class TodoService : ITodoService
	{
		private readonly DataContext _dbContext;
		public TodoService(DataContext dbContext)
		{
			_dbContext = dbContext;
		}

		private GetTask ConvertDTO(Todo task) 
		{
			return new GetTask 
			{
				Id = task.Id,
				isCompleted = task.IsCompleted,
				Title = task.Title,
			};
		}
		public async Task<GetTask> AddTask(CreateTask request)
		{
			if (string.IsNullOrEmpty(request.Title)) 
			{
				return new GetTask();
			}

			Todo newTask = new Todo 
			{
				Title = request.Title,
				IsCompleted = false,
			};

			_dbContext.Todos.Add(newTask);

			await _dbContext.SaveChangesAsync();

			return ConvertDTO(newTask);
		}

		public async Task<GetTask?> GetSingleTask(int Id)
		{
			Todo? singleTask = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == Id);

			if (singleTask == null) return null;

			return ConvertDTO(singleTask);
		}

		public async Task<List<GetTask>> GetAllTask()
		{
			List<Todo> todos = await _dbContext.Todos.ToListAsync();

			return todos.Select(ConvertDTO).ToList();
		}

		public async Task<int> RemoveTask(int taskId)
		{
			Todo? task = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == taskId);

			if (task == null) return 0;	

			 _dbContext.Remove(task);

			return await _dbContext.SaveChangesAsync();

		}

		public async Task<int> UpdateTask(CreateTask request, int taskId)
		{
			Todo? task = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == taskId);

			if (task == null) return 0;

			task.Title = request.Title;
			task.IsCompleted = request.IsCompleted;

			return await _dbContext.SaveChangesAsync();
		}

		public async Task<int> UpdateStatus(int taskId)
		{
			var task = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == taskId);

			if (task == null) return 0;

			task.IsCompleted = !task.IsCompleted;

			return await _dbContext.SaveChangesAsync();
		}
	}
}
