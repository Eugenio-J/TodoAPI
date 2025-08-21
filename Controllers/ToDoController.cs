using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoController : ControllerBase
	{
		private readonly ITodoService _todoService;
		public ToDoController(ITodoService todoService)
		{
			_todoService = todoService;
		}

		[HttpGet("get-single-task/{Id}")]
		public async Task<ActionResult<int>> GetSingleTask(int Id) 
		{
			var result = await _todoService.GetSingleTask(Id);
			if(result == null) return NotFound();
			return Ok(result);
		}

		[HttpGet("get-all-task")]
		public async Task<ActionResult<List<GetTask>>> GetAllTask() 
		{
			var result = await _todoService.GetAllTask();
			if(result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost("add-task")]
		public async Task<ActionResult<int>> AddTask(CreateTask request) 
		{
			var result = await _todoService.AddTask(request);
			if (result == 0) return BadRequest();
			return Ok(result);
		}

		[HttpPut("update-task")]
		public async Task<ActionResult<int>> UpdateTask(CreateTask request, [FromQuery] int taskId) 
		{
			var result = await _todoService.UpdateTask(request, taskId);
			if (result == 0) return BadRequest(); 
			return Ok(result);
		}

		[HttpDelete("delete-task")]	
		public async Task<ActionResult<int>> RemoveTask([FromQuery] int taskId)
		{
			var result = await _todoService.RemoveTask(taskId);
			if (result == 0) return BadRequest();
			return Ok(result);
		}
	}
}
