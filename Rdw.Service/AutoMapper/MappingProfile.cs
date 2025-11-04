using AutoMapper;
using Rdw.ApiWrapper.Models;
using Rdw.Repository.Entities;
using Rdw.Service.Models;

namespace Rdw.Service.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vehicle, VehicleModel>()
            .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.ModelYear))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.FuelInfo != null ? src.FuelInfo.FuelType : null))
            .ForMember(dest => dest.FuelConsumptionLiters, opt => opt.MapFrom(src =>
                src.FuelInfo != null
                    ? (src.FuelInfo.FuelConsumptionLitersWltp
                        ?? src.FuelInfo.FuelConsumptionLitersNedc
                        ?? src.FuelInfo.FuelConsumptionLitersWeightedWltp
                        ?? src.FuelInfo.FuelConsumptionLitersWeightedCombinedWltp)
                    : null))
            .ForMember(dest => dest.CarbonEmissionGramKm, opt => opt.MapFrom(src =>
                src.FuelInfo != null
                    ? (src.FuelInfo.CarbonEmissionGramWltp
                        ?? src.FuelInfo.CarbonEmissionGramNedc
                        ?? src.FuelInfo.CarbonEmissionGramWeighted
                        ?? src.FuelInfo.CarbonEmissionGramWeightedWltp)
                    : null))
            .ForMember(dest => dest.ElectricConsumptionWh, opt => opt.MapFrom(src =>
                src.FuelInfo != null
                    ? (src.FuelInfo.ElectricConsumptionWhSingleWltp
                        ?? src.FuelInfo.ElectricConsumptionWhExternChargingWltp)
                    : null));

        CreateMap<Vehicle, VehicleEntity>()
            .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.ModelYear))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.FuelInfo != null ? src.FuelInfo.FuelType : null))
            .ForMember(dest => dest.FuelConsumptionLiters, opt => opt.MapFrom(src =>
                src.FuelInfo != null
                    ? (src.FuelInfo.FuelConsumptionLitersWltp
                        ?? src.FuelInfo.FuelConsumptionLitersNedc
                        ?? src.FuelInfo.FuelConsumptionLitersWeightedWltp
                        ?? src.FuelInfo.FuelConsumptionLitersWeightedCombinedWltp)
                    : null))
            .ForMember(dest => dest.CarbonEmissionGramKm, opt => opt.MapFrom(src =>
                src.FuelInfo != null
                    ? (src.FuelInfo.CarbonEmissionGramWltp
                        ?? src.FuelInfo.CarbonEmissionGramNedc
                        ?? src.FuelInfo.CarbonEmissionGramWeighted
                        ?? src.FuelInfo.CarbonEmissionGramWeightedWltp)
                    : null))
            .ForMember(dest => dest.ElectricConsumptionWh, opt => opt.MapFrom(src =>
                src.FuelInfo != null
                    ? (src.FuelInfo.ElectricConsumptionWhSingleWltp
                        ?? src.FuelInfo.ElectricConsumptionWhExternChargingWltp)
                    : null));

        CreateMap<VehicleModel, VehicleEntity>();
    }
}
