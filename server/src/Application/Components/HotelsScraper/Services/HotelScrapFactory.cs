using System;
using System.Collections.Generic;
using System.Dynamic;
using Application.Components.HotelsScraper.Abstractions;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Components.HotelsScraper.Services
{
    [As(typeof(IHotelScrapFactory))]
    public class HotelScrapFactory : IHotelScrapFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public HotelScrapFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ExpandoObject Create(string htmlContent)
        {
            var hotel = new ExpandoObject();

            var hotelScrapers = _serviceProvider.GetServices<IHotelScraper>();

            foreach (var hotelScraper in hotelScrapers)
            {
                hotel.TryAdd(hotelScraper.Key, hotelScraper.Extract(htmlContent));
            }

            return hotel;
        }
    }
}