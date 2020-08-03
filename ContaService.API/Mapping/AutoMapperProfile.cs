using AutoMapper;
using System;
using ContaService.API.ViewModels;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;

namespace ContaService.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Models.Lancamento, LancamentoDetalhe>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ForMember(dest => dest.Operacao, opt => opt.MapFrom(src => src.TipoOperacao.GetDescription()));
        }
    }
}