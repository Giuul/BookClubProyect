using Domain.Entities;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "usuario";
        public string? Password { get; set; }

        public List<ReadingListDTO> Listas { get; set; } = new List<ReadingListDTO>();

        public static UserDTO Create(User user) => new UserDTO
        {
            Id = user.Id,
            Nombre = user.Nombre,
            Email = user.Email,
            Rol = user.Rol.ToString(),
            Listas = user.ListasCreadas?.Select(ReadingListDTO.Create).ToList() ?? new List<ReadingListDTO>()
        };

        public static List<UserDTO> CreateList(IEnumerable<User> users)
            => users.Select(Create).ToList();
    }
}

