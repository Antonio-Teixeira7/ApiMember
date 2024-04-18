using ApiMember.Data.Contexts;
using ApiMember.Models;

namespace ApiMember.Data.Dtos.CellDtos;

public class ReadCellDto
{
    public string? Name { get; set; }

    public string? Day { get; set; }

    public string? Hour { get; set; }

    public string? Leader { get; set; }

    public List<string>? Members { get; set; }       

    public ReadCellDto MapperToRead(Cell cell, MemberContext memberContext)
    {
        ReadCellDto dto = new()
        {
            Name = cell.Name,
            Day = cell.Day,
            Hour = cell.Hour,
            Leader = memberContext.Members.FirstOrDefault(member => member.Id == cell.LeaderId)?.Name ?? "No Content"
        };
        List<string> names = [];
        foreach (var id in cell.MembersId)
        {
            var member = memberContext.Members.FirstOrDefault(m => m.Id == id);
            names.Add(member!.Name);
        }
        dto.Members = names;
        return dto;
    }

    public IEnumerable<ReadCellDto> MapperToListDto(MemberContext memberContext, CellContext cellContetex)
    {
        List<ReadCellDto> listDto = [];
        var listCell = cellContetex.Cells.ToList();
        foreach (var cell in listCell)
        {
            listDto.Add(MapperToRead(cell, memberContext));
        }
        return listDto;
    }

    public List<string> MapperGetMemberInCell(Cell cell, MemberContext memberContext)
    {
        var listInt = cell.MembersId.ToList();
        List<string> memberNames = [];
        foreach (var number in listInt)
        {
            var member = memberContext.Members.FirstOrDefault(x => x.Id == number);
            memberNames.Add(member!.Name);
        }
        return memberNames;
    }
}
