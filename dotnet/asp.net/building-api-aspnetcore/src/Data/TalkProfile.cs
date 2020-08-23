using AutoMapper;
using CoreCodeCamp.Models;

namespace CoreCodeCamp.Data
{
    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(x => x.Camp, opt => opt.Ignore())
                .ForMember(x => x.Speaker, opt => opt.Ignore());
        }
    }
}