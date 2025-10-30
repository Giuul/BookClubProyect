using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "usuario";

        public static UserDTO Create(User user) => new UserDTO
           {
               Id = user.Id,
               Nombre = user.Nombre,
               Email = user.Email,
               Rol = user.Rol.ToString()
           };

        public static List<UserDTO> CreateList(IEnumerable<User> users)
            => users.Select(Create).ToList();
    }


}

