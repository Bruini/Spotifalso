using AutoMapper;
using Spotifalso.Aplication.ViewModels;
using Spotifalso.Core.Models;

namespace Spotifalso.Aplication.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
