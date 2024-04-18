using ApiMember.Data.Contexts;
using ApiMember.Data.Dtos.MemberDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiMember.Controllers;

[ApiController]
[Route("[Controller]")]
public class MemberController : ControllerBase
{
    private MemberContext _memberContext;
    private CellContext _cellContext;
    public MemberController(MemberContext memberContext, CellContext cellContext)
    {
        _memberContext = memberContext;
        _cellContext = cellContext;
    }

    [HttpPost("Create Member")]
    public IActionResult CreateMember([FromBody] CreateMemberDto memberDto)
    {
        if (memberDto.Exist(memberDto, _memberContext)) return NotFound();
        var member = memberDto.MapperToMember(memberDto);
        _memberContext.Members.Add(member);
        _memberContext.SaveChanges();
        return Created();
    }

    [HttpGet("Get All")]
    public IEnumerable<ReadMemberDto> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        ReadMemberDto dto = new();
        var listDto = dto.MapperToListRead(_memberContext, _cellContext);
        return listDto.Skip(skip).Take(take);
    }

    [HttpGet("Get member by/{id}")]
    public IActionResult GetMemberById(int id)
    {
        var member = _memberContext.Members.FirstOrDefault(x => x.Id == id);
        if (member == null) return NotFound();
        ReadMemberDto dto = new();
        var readDto = dto.MapperToRead(member, _memberContext, _cellContext);
        return Ok(readDto);
    }

    [HttpGet("Get Id By Name And Last Name")]
    public IActionResult GetIdByName([FromQuery] string name, [FromQuery] string lastName)
    {
        var member = _memberContext.Members.FirstOrDefault(x => x.Name == name.ToLower() && x.LastName == lastName.ToLower());
        if (member == null) return NotFound();
        return Ok("Id: " + member.Id);
    }

    [HttpPut("Update Member")]
    public IActionResult UpDateMember([FromQuery] int idMember, UpdateMemberDto memberDto)
    {
        var memberOn = _memberContext.Members.FirstOrDefault(y => y.Id == idMember);
        if (memberOn == null) return NotFound();
        memberDto.MapperUpdateDto(memberOn, memberDto);
        _memberContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("Add Disciple To Leader")]
    public IActionResult AddDisciple([FromQuery] int idLeader, [FromQuery] int idDisciple)
    {
        var leader = _memberContext.Members.FirstOrDefault(x => x.Id == idLeader);
        if (leader == null) return NotFound();
        var disciple = _memberContext.Members.FirstOrDefault(x => x.Id == idDisciple);
        if (disciple == null) return NotFound();
        foreach (var member in _memberContext.Members)
        {
            if (member.DisciplesId.Contains(idDisciple))
            {
                return NotFound();
            }
            else
            {
                leader.DisciplesId.Add(idDisciple);
                break;
            }
        }
        _memberContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("Remove Discipile In Leader")]
    public IActionResult RemoveDiscipleInMember([FromQuery] int idLeader, [FromQuery] int idDisciple)
    {
        var leader = _memberContext.Members.FirstOrDefault(x => x.Id == idLeader);
        if (leader == null) return NotFound();
        var disciple = _memberContext.Members.FirstOrDefault(_ => _.Id == idDisciple);
        if (disciple == null) return NotFound();
        if (!leader.DisciplesId.Contains(idDisciple))
        {
            return NotFound();
        }
        leader.DisciplesId.Remove(idDisciple);
        _memberContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("Delete Member")]
    public IActionResult DeleteMember([FromQuery] int idMember)
    {
        var member = _memberContext.Members.FirstOrDefault(m => m.Id == idMember);
        if (member == null) return NotFound();
        member.HelpOfDelete(_memberContext, _cellContext, idMember);
        _memberContext.Members.Remove(member);
        _memberContext.SaveChanges();
        _cellContext.SaveChanges();
        return NoContent();
    }
}