using ApiMember.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiMember.Data.Dtos.CellDtos;

public class UpdateCellDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Day { get; set; }

    [Required]
    public string Hour { get; set; }

    public void MapperToUpdate(Cell cell, UpdateCellDto updateCellDto)
    {
        cell.Name = updateCellDto.Name;
        cell.Day = updateCellDto.Day;
        cell.Hour = updateCellDto.Hour;
    }
}

