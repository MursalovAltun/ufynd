using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRatesReports.Abstractions;
using Application.Components.HotelRatesReports.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using AutoMapper;
using ClosedXML.Excel;

namespace Application.Components.HotelRatesReports.Services
{
    [As(typeof(IHotelRatesExcelReportBuilder))]
    public class HotelRatesExcelReportBuilder : IHotelRatesExcelReportBuilder
    {
        private readonly IMapper _mapper;

        private readonly IEnumerable<string> _headerRow = new ReadOnlyCollection<string>(
            new[]
                {"Arrival date", "Departure date", "Price", "Currency", "Rate name", "Adults", "Breakfast included"}
        );

        public HotelRatesExcelReportBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public byte[] Build(IReadOnlyCollection<HotelWithRatesDto> hotelWithRates)
        {
            var workbook = new XLWorkbook();

            var worksheet = workbook.AddWorksheet("Report");

            BuildHeader(worksheet);

            BuildTable(worksheet, hotelWithRates);

            return Save(workbook);
        }

        private void BuildHeader(IXLWorksheet worksheet)
        {
            var header = worksheet.Cell(1, 1).InsertData(_headerRow, true);

            header.Style.Font.Bold = true;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void BuildTable(IXLWorksheet worksheet, IReadOnlyCollection<HotelWithRatesDto> hotelWithRates)
        {
            if (hotelWithRates is not {Count: > 0}) return;

            var hotelsRates = hotelWithRates.SelectMany(x => x.HotelRates).ToList();

            var tableData = _mapper.Map<IEnumerable<HotelRatesExcelReport>>(hotelsRates);

            worksheet.Cell(2, 1)
                .InsertData(tableData);

            worksheet.Columns().AdjustToContents();
        }

        private byte[] Save(IXLWorkbook workbook)
        {
            using var ms = new MemoryStream();

            workbook.SaveAs(ms);

            return ms.ToArray();
        }
    }
}