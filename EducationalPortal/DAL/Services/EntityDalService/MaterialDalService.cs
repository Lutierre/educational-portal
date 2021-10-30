using System;
using System.Collections.Generic;
using AutoMapper;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;
using DTO.Models;
using DTO.Models.Materials;

namespace DAL.Services.EntityDalService
{
    public class MaterialDalService : IEntityDalService<Material>
    {
        private readonly IGenericDalService<MaterialDto> _materialDalService;
        private readonly IGenericDalService<ArticleDto> _articleDtoService;
        private readonly IGenericDalService<BookDto> _bookDtoService;
        private readonly IGenericDalService<VideoDto> _videoDtoService;

        private readonly IMapper _mapper;

        public MaterialDalService(IGenericDalService<MaterialDto> materialDalService,
            IGenericDalService<ArticleDto> articleDtoService,
            IGenericDalService<BookDto> bookDtoService,
            IGenericDalService<VideoDto> videoDtoService)
        {
            _materialDalService = materialDalService;
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
        
        private T1 TryGetMaterial<T1, T2>(IGenericDalService<T2> genericDalService, int id)
            where T1 : Material where T2 : BaseEntityDto 
        {
            var dtos = genericDalService.Filter(dto => dto.Id == id);

            if (dtos.Count == 0)
            {
                return null;
            }
            
            var dto = _materialDalService.Get(id);
            var material = _mapper.Map<Material>(dto);
            var result = _mapper.Map<T1>(dtos[0]);

            result.Id = id;
            result.Title = material.Title;

            return result;
        }

        private void TryUpdateMaterial<T>(IGenericDalService<T> genericDalService, Material material)
            where T : AbstractMaterialDto
        {
            var dtos = genericDalService.Filter(dto => dto.MaterialId == material.Id);

            var dto = _mapper.Map<T>(material);
            dto.Id = dtos[0].Id;

            genericDalService.Update(dto);
        }

        public void Add(Material material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            var id = _materialDalService.Add(dto);
            
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

        public void Update(Material material)
        {
            var dto = _mapper.Map<MaterialDto>(material);
            _materialDalService.Update(dto);

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

            _materialDalService.Delete(id);
        }
    }
}
