using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Model.ModelsDto;

namespace Hiberus.Model.MapperModels
{
    public class MapperModel : AutoMapper.Profile
    {
        public MapperModel()
        {
            CreateMap<TransactionDto, Transaction>().ReverseMap();
        }
    }
}
