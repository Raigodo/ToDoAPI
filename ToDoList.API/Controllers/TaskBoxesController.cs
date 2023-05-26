using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using ToDoList.API.Services.Check;
using System.Threading.Tasks;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TaskBoxesController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;

    public TaskBoxesController(
        ApiDbContext appDbContext,
        UserManager<UserEntity> userManager,
        IAcessGuardService acessCheck,
        ICheckExistingRecordService existCheck)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
        _acessCheck = acessCheck;
        _existCheck = existCheck;
    }

    [HttpGet]
    [Authorize]
    [Route("GetRoot")]
    public async Task<IActionResult> GetRoot(int boxId)
    {
        if (!(await _existCheck.DoesBoxExistAsync(boxId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsBoxAcessibleAsync(boxId)))
            return Unauthorized("Acess denied");


        var rootBoxes = await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .Where(b => b.AssociatedGroupId == boxId && b.ParrentBoxId == null)
            .ToListAsync();

        return Ok(rootBoxes);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int boxId)
    {
        if (!(await _existCheck.DoesBoxExistAsync(boxId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsBoxAcessibleAsync(boxId)))
            return Unauthorized("Acess denied");


        var box = await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .FirstOrDefaultAsync(b=>b.Id == boxId);

        return Ok(box);
    }

    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(TaskBoxDto entityDto)
    {
        if (!(await _existCheck.DoesGroupExistAsync(entityDto.AssociatedGroupId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupAcessibleAsync(entityDto.AssociatedGroupId)))
            return Unauthorized("Acess denied");


        var entity = new TaskBoxEntity()
        {
            AssociatedGroupId = entityDto.AssociatedGroupId,
            ParrentBoxId = entityDto.ParrentBoxId,
            Title = entityDto.Title,
        };
        await _dbCtx.ApiTaskBoxes.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return CreatedAtAction("Get", new { entity.Id }, entity);
    }
    

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int boxId, TaskBoxDto entityDto)
    {
        if (await _existCheck.DoesBoxExistAsync(boxId) ||
            (entityDto.ParrentBoxId != null && await _existCheck.DoesBoxExistAsync((int) entityDto.ParrentBoxId)) ||
            !(await _existCheck.DoesGroupExistAsync(entityDto.AssociatedGroupId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupAcessibleAsync(entityDto.AssociatedGroupId)))
            return Unauthorized("Acess denied");


        var box = await _dbCtx.ApiTaskBoxes.FirstOrDefaultAsync(b => b.Id == boxId);
        if (box == null)
            return BadRequest();

        box.Title = entityDto.Title;
        box.AssociatedGroupId = entityDto.AssociatedGroupId; 
        box.ParrentBoxId = entityDto.ParrentBoxId;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
    

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int boxId)
    {
        if (!(await _existCheck.DoesGroupExistAsync(boxId)))
            return BadRequest("Invalid Id");

        if (await _acessCheck.IsGroupAcessibleAsync(boxId))
            return Unauthorized("Acess denied");


        var box = await _dbCtx.ApiTaskBoxes.FirstOrDefaultAsync(b => b.Id == boxId);
        if (box == null)
            return BadRequest();

        _dbCtx.ApiTaskBoxes.Remove(box);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
