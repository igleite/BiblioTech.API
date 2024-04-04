using BiblioTech.API.Entities;
using BiblioTech.API.Models.ViewModels;

namespace BiblioTech.API.MappingViewModels
{
    public static class MappingUsersViewModel
    {
        public static IEnumerable<UserViewModel> ConvertUserToViewModel(
            this IEnumerable<User> users)
        {
            return (from user in users
                    select new UserViewModel
                    {
                        Id = user.Id,
                        Cpf = user.Cpf,
                        Name = user.Name,
                        Email = user.Email,
                        BlockedDate = user.BlockedDate,

                    }).ToList();
        }

        public static UserViewModel ConvertUserByIdViewModel(
            this User users)
        {
            return new UserViewModel
            {
                Id = users.Id,
                Cpf = users.Cpf,
                Name = users.Name,
                Email = users.Email,
                BlockedDate = users.BlockedDate,
            };
        }

    }  
}
