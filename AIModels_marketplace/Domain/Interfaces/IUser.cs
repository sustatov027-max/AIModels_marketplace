using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        string Username { get; set; }
        string PasswordHash { get; set; }
        string Role { get; set; }
    }
}
