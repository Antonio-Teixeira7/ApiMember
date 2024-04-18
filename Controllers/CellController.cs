using ApiMember.Data.Contexts;
using ApiMember.Data.Dtos.CellDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiMember.Controllers;

[ApiController]
[Route("[Controller]")]
public class CellController : ControllerBase
{
    private CellContext _cellContext;
    private MemberContext _memberContext;
    public CellController(CellContext cellContext, MemberContext memberContext)
    {
        _cellContext = cellContext;
        _memberContext = memberContext;
    }

    [HttpPost("Create Cell")]
    public IActionResult CreateCell([FromBody] CreateCellDto cellDto)
    {
        var cell = cellDto.MapperToCell(cellDto);
        _cellContext.Cells.Add(cell);
        _cellContext.SaveChanges();
        return Created();
    }

    [HttpGet("Get All Cells")]
    public IEnumerable<ReadCellDto> GetAllCells()
    {
        ReadCellDto dto = new();
        var cellListDto = dto.MapperToListDto(_memberContext, _cellContext);
        return cellListDto;
    }

    [HttpGet("Get Cell by Id/{id}")]
    public IActionResult GetCellById(int id)
    {
        var cell = _cellContext.Cells.FirstOrDefault(cell => cell.Id == id);
        if (cell == null) return NotFound();
        ReadCellDto readDto = new();
        var dto = readDto.MapperToRead(cell, _memberContext);
        return Ok(dto);
    }

    [HttpGet("Get members to cell/{id}")]
    public IActionResult GetMembersToCell(int id)
    {
        var cell = _cellContext.Cells.FirstOrDefault(cell => cell.Id == id);
        if (cell == null) return NotFound();
        ReadCellDto dto = new();
        var namesListDto = dto.MapperGetMemberInCell(cell, _memberContext);
        return Ok(namesListDto);
    }

    [HttpPut("Update Cell/{id}")]
    public IActionResult UpdateCell(int id, [FromBody] UpdateCellDto cellDto)
    {
        var cell = _cellContext.Cells.FirstOrDefault(x => x.Id == id);
        if (cell == null) return NotFound();
        cellDto.MapperToUpdate(cell, cellDto);
        _cellContext.SaveChanges();
        return Ok();
    }

    [HttpPatch("Add leader to cell")]
    public IActionResult AddLeaderToCell([FromQuery] int idLeader, [FromQuery] int idCell)
    {
        var leader = _memberContext.Members.FirstOrDefault(x => x.Id == idLeader);
        if (leader == null) NotFound();
        var cell = _cellContext.Cells.FirstOrDefault(x => x.Id == idCell);
        if (cell == null) NotFound();
        cell!.LeaderId = idLeader;
        _cellContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("Add member to cell")]
    public IActionResult AddMemberToCell([FromQuery] int idMember, [FromQuery] int idCell)
    {
        var member = _memberContext.Members.FirstOrDefault(x => x.Id == idMember);
        if (member == null) return NotFound();
        var cell = _cellContext.Cells.FirstOrDefault(x => x.Id == idCell);
        if (cell == null) return NotFound();
        foreach (var cellDto in _cellContext.Cells)
        {
            if (cellDto.MembersId.Contains(idMember))
            {
                return NotFound();
            }
        }
        cell.MembersId.Add(idMember);
        _cellContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("Remove Member In Cell/{id}")]
    public IActionResult RemoveMemberInCell(int id, [FromQuery] int idMember)
    {
        var cell = _cellContext.Cells.FirstOrDefault(cell => cell.Id == id);
        if (cell == null) return NotFound();
        var member = _memberContext.Members.FirstOrDefault(x => x.Id == idMember);
        if (member == null) NotFound();
        cell.MembersId.Remove(idMember);
        _cellContext.SaveChanges();
        return NoContent();
    }


    [HttpDelete("Delete Cell/{idCell}")]
    public IActionResult DeleteCell(int idCell)
    {
        var cell = _cellContext.Cells.FirstOrDefault(_x => _x.Id == idCell);
        if (cell == null) return NotFound();
        _cellContext.Cells.Remove(cell);
        _cellContext.SaveChanges();
        return NoContent();
    }
}
