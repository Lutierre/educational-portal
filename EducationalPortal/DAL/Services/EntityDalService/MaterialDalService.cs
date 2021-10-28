using AutoMapper;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;

namespace DAL.Services.EntityDalService
{
    public class MaterialDalService : IEntityDalService<Material>
    {
        private readonly IGenericDalService<MaterialDto> _genericDalService;

        private readonly IMapper _mapper;

        public MaterialDalService(IGenericDalService<MaterialDto> genericDalService)
        {
            _genericDalService = genericDalService;
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MaterialDto, Material>();
                cfg.CreateMap<Material, MaterialDto>();
            });
                
            _mapper = config.CreateMapper();
        }
        
        public void Add(Material material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            _genericDalService.Add(dto);
        }

        public Material Get(int id)
        {
            var dto = _genericDalService.Get(id);
            var material = _mapper.Map<Material>(dto);
            
            // TODO: enhance mapping / fill collections

            return material;
        }

        public void Update(Material material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            _genericDalService.Update(dto);
            
            // TODO: process collections in Material
        }

        public void Delete(int id)
        {
            _genericDalService.Delete(id);
        }
    }
}
