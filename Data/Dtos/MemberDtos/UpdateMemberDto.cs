using ApiMember.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiMember.Data.Dtos.MemberDtos;

public class UpdateMemberDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public string Sex { get; set; }

    public void MapperUpdateDto(Member member, UpdateMemberDto updateMemberDto)
    {
        member.Name = updateMemberDto.Name;
        member.LastName = updateMemberDto.LastName;
        member.Age = updateMemberDto.Age;
        member.Sex = updateMemberDto.Sex;
    }
}
