using System.ComponentModel.DataAnnotations;

namespace ApiMember.Models;

public class Cell
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Day { get; set; }

    [Required]
    public string Hour { get; set; }

    [Required]
    public int LeaderId { get; set; }

    [Required]
    public List<int> MembersId { get; set; }
}
