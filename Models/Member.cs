using ApiMember.Data.Contexts;
using System.ComponentModel.DataAnnotations;

namespace ApiMember.Models
{
    public class Member
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public List<int> DisciplesId { get; set; }

        public void HelpOfDelete(MemberContext memberContext, CellContext cellContext, int idMember)
        {
            foreach (var member in memberContext.Members)
            {
                if (member.DisciplesId.Contains(idMember))
                {
                    member.DisciplesId.Remove(idMember); break;
                }
            }
            foreach (var cell in cellContext.Cells)
            {
                if (cell.MembersId.Contains(idMember))
                {
                    cell.MembersId.Remove(idMember); break;
                }
            }
        }
    }
}
