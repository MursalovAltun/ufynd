using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Components.EmailSender;
using Application.Components.HotelRatesReports.Abstractions;
using Application.Components.HotelRatesReports.Options;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using Microsoft.Extensions.Options;

namespace Application.Components.HotelRatesReports.Services
{
    [As(typeof(IHotelRatesExcelReportEmailNotificationService))]
    public class HotelRatesExcelReportEmailNotificationService : IHotelRatesExcelReportEmailNotificationService
    {
        private readonly IEmailService _emailService;
        private readonly HotelRatesReportJobOptions _options;

        public HotelRatesExcelReportEmailNotificationService(IOptions<HotelRatesReportJobOptions> options,
            IEmailService emailService)
        {
            _emailService = emailService;
            _options = options.Value;
        }

        public async Task SendAsync(byte[] report)
        {
            var reportAttachment = new SendEmailRequestAttachment
            {
                Filename = _options.ReportFileName,
                Content = Convert.ToBase64String(report),
                Type = _options.ReportContentType
            };

            await _emailService.SendAsync(new SendMultipleEmailsRequest
            {
                Emails = _options.EmailsToSend,
                Subject = _options.Subject,
                Content = _options.Content,
                Attachments = new List<SendEmailRequestAttachment> {reportAttachment}
            });
        }
    }
}