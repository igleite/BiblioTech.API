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
                        Name = user.Name,

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
            };
        }

    }  
}
