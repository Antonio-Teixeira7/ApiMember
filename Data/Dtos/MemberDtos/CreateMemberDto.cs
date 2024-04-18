using ApiMember.Data.Contexts;
using ApiMember.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiMember.Data.Dtos.MemberDtos;

public class CreateMemberDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public string Sex { get; set; }

    public Member MapperToMember(CreateMemberDto dto)
    {
        Member member = new()
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Age = dto.Age,
            Sex = dto.Sex,
            DisciplesId = []
        };
        return member;
    }

    public bool Exist(CreateMemberDto member, MemberContext memberContext)
    {
        foreach(Member item in memberContext.Members)
        {
            if (item.Name.ToLower() == member.Name && item.LastName.ToLower() == member.LastName)
            {
                return true;
            }
            continue;
        }
        return false;
    }
}
