using System.Collections.Generic;

namespace Application.Components.HotelRatesReports.Options
{
    public class HotelRatesReportJobOptions
    {
        public string JobId { get; set; }
        public string CronInterval { get; set; }
        public List<string> EmailsToSend { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReportFileName { get; set; }

        public string ReportContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}