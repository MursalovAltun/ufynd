using System.Linq;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRatesReports.Models;
using AutoMapper;

namespace Application.Components.HotelRatesReports.Profiles
{
    public class HotelRatesExcelReportProfile : Profile
    {
        public HotelRatesExcelReportProfile()
        {
            CreateMap<HotelRateDto, HotelRatesExcelReport>()
                .ForMember(m => m.ArrivalDate, opt =>
                    opt.MapFrom(src => src.TargetDay.Date))
                .ForMember(m => m.DepartureDate, opt =>
                    opt.MapFrom(src => src.TargetDay.AddDays(src.Los).Date))
                .ForMember(m => m.Price, opt =>
                    opt.MapFrom(src => src.Price.NumericFloat))
                .ForMember(m => m.BreakfastIncluded, opt =>
                    opt.MapFrom(src => src.RateTags.Any(tag => tag.Name.ToLower() == "breakfast" && tag.Shape) ? "Yes" : "No"));
        }
    }
}