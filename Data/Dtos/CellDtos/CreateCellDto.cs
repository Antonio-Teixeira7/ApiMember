using ApiMember.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiMember.Data.Dtos.CellDtos
{
    public class CreateCellDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public string Hour { get; set; }

        public Cell MapperToCell(CreateCellDto cellDto)
        {
            Cell cell = new();
            cell.Name = cellDto.Name;
            cell.Day = cellDto.Day;
            cell.Hour = cellDto.Hour;
            cell.LeaderId = 0;
            cell.MembersId = [];
            return cell;
        }
    }
}
