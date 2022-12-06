using AutoMapper;

namespace Application.Mappings
{
    public interface IMapTo<T> : IMapped
    {
        void IMapped.Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
