using BrainWave.Application.Features.Tasks.Commands.CreateTask;
using BrainWave.Application.Features.Tasks.Commands.DeleteTask;
using BrainWave.Application.Features.Tasks.Commands.UpdateTask;
using BrainWave.Application.Features.Tasks.Queries.GetTasks;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

public class TasksController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetTasks([FromQuery] Guid userId)
    {
        return await Mediator.Send(new GetTasksQuery { UserId = userId });
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTaskCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTaskCommand command)
    {
        if (id != command.Id) return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteTaskCommand(id));

        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<bool>> CompleteTask(Guid id, BrainWave.Application.Features.Tasks.Commands.CompleteTask.CompleteTaskCommand command)
    {
        if (id != command.Id) return BadRequest();
        var success = await Mediator.Send(command);
        if (!success) return NotFound();
        return Ok(success);
    }
}
