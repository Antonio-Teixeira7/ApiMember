using ApiMember.Data.Contexts;
using ApiMember.Models;

namespace ApiMember.Data.Dtos.MemberDtos;

public class ReadMemberDto
{
    public string Name { get; set; }

    public string LastName { get; set; }

    public int Age { get; set; }

    public string Sex { get; set; }

    public List<string> DisciplesName { get; set; }

    public string Cell { get; set; }

    public ReadMemberDto MapperToRead(Member member, MemberContext memberContext, CellContext cellContext)
    {
        ReadMemberDto dto = new()
        {
            Name = member.Name,
            LastName = member.LastName,
            Age = member.Age,
            Sex = member.Sex
        };
        List<string> discipilesName = [];
        foreach (int id in member.DisciplesId)
        {
            discipilesName.Add(memberContext.Members.FirstOrDefault(x => x.Id == id)!.Name);
        }
        dto.DisciplesName = discipilesName;
        foreach (Cell item in cellContext.Cells)
        {
            if (item.MembersId.Contains(member.Id))
            {
                dto.Cell = item.Name;
                break;
            }
        }
        return dto;
    }

    public List<ReadMemberDto> MapperToListRead(MemberContext memberContext, CellContext cellContext)
    {
        List<ReadMemberDto> listDto = [];
        var listMember = memberContext.Members.ToList();
        foreach (Member member in listMember)
        {
            ReadMemberDto readMemberDto = new();
            var memberDto = readMemberDto.MapperToRead(member, memberContext, cellContext);
            listDto.Add(memberDto);
        }
        return listDto;
    }
}
