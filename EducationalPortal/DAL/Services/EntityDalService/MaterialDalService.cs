using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;
using DTO.Models;
using DTO.Models.Materials;

namespace DAL.Services.EntityDalService
{
    public class MaterialDalService : IEntityDalService<Material>
    {
        private readonly IGenericDtoService<MaterialDto> _materialDtoService;
        private readonly IGenericDtoService<ArticleDto> _articleDtoService;
        private readonly IGenericDtoService<BookDto> _bookDtoService;
        private readonly IGenericDtoService<VideoDto> _videoDtoService;

        private readonly IMapper _mapper;

        public MaterialDalService(IGenericDtoService<MaterialDto> materialDtoService,
            IGenericDtoService<ArticleDto> articleDtoService,
            IGenericDtoService<BookDto> bookDtoService,
            IGenericDtoService<VideoDto> videoDtoService)
        {
            _materialDtoService = materialDtoService;
            _articleDtoService = articleDtoService;
            _bookDtoService = bookDtoService;
            _videoDtoService = videoDtoService;
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MaterialDto, Material>();
                cfg.CreateMap<Material, MaterialDto>();
                cfg.CreateMap<ArticleDto, Article>();
                cfg.CreateMap<BookDto, Book>();
                cfg.CreateMap<VideoDto, Video>();
            });
                
            _mapper = config.CreateMapper();
        }
        
        private T1 TryGetMaterial<T1, T2>(IGenericDtoService<T2> genericDtoService, int id)
            where T1 : Material where T2 : BaseEntityDto 
        {
            var dtos = genericDtoService.Filter(dto => dto.Id == id);

            if (dtos.Count == 0)
            {
                return null;
            }
            
            var dto = _materialDtoService.Get(id);
            var material = _mapper.Map<Material>(dto);
            var result = _mapper.Map<T1>(dtos[0]);

            result.Id = id;
            result.Title = material.Title;

            return result;
        }

        private void TryUpdateMaterial<T>(IGenericDtoService<T> genericDtoService, Material material)
            where T : AbstractMaterialDto
        {
            var dtos = genericDtoService.Filter(dto => dto.MaterialId == material.Id);

            var dto = _mapper.Map<T>(material);
            dto.Id = dtos[0].Id;

            genericDtoService.Update(dto);
        }

        public Material Add(Material material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            var id = _materialDtoService.Add(dto);
            
            switch (material.Type)
            {
                case "Article":
                {
                    var articleDto = _mapper.Map<ArticleDto>(material);
                    articleDto.MaterialId = id;
                    _articleDtoService.Add(articleDto);
                    break;
                }
                case "Book":
                {
                    var bookDto = _mapper.Map<BookDto>(material);
                    bookDto.MaterialId = id;
                    _bookDtoService.Add(bookDto);
                    break;
                }
                case "Video":
                {
                    var videoDto = _mapper.Map<VideoDto>(material);
                    videoDto.MaterialId = id;
                    _videoDtoService.Add(videoDto);
                    break;
                }
            }

            var result = Get(id);

            return result;
        }

        public Material Get(int id)
        {
            var article = TryGetMaterial<Article, ArticleDto>(_articleDtoService, id);
            
            if (article != null)
            {
                return article;
            }

            var book = TryGetMaterial<Book, BookDto>(_bookDtoService, id);
            
            if (book != null)
            {
                return book;
            }

            var video = TryGetMaterial<Video, VideoDto>(_videoDtoService, id);
            
            if (video != null)
            {
                return video;
            }

            return null;
        }

        public List<Material> Filter(Func<Material, bool> criteriaFunc)
        {
            var materialDtos = 
                _materialDtoService.Filter(materialDto => criteriaFunc(_mapper.Map<Material>(materialDto)));
            var filteredMaterials =
                materialDtos.Select(materialDto => _mapper.Map<Material>(materialDto)).ToList();

            return filteredMaterials;
        }

        public void Update(Material material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            _materialDtoService.Update(dto);

            switch (material.Type)
            {
                case "Article":
                    TryUpdateMaterial(_articleDtoService, material);
                    break;
                case "Book":
                    TryUpdateMaterial(_bookDtoService, material);
                    break;
                case "Video":
                    TryUpdateMaterial(_videoDtoService, material);
                    break;
            }
        }

        public void Delete(int id)
        {
            var material = Get(id);

            switch (material.Type)
            {
                case "Article":
                    _articleDtoService.DeleteMany(dto => dto.MaterialId == id);
                    break;
                case "Book":
                    _bookDtoService.DeleteMany(dto => dto.MaterialId == id);
                    break;
                case "Video":
                    _videoDtoService.DeleteMany(dto => dto.MaterialId == id);
                    break;
            }

            _materialDtoService.Delete(id);
        }
    }
}
