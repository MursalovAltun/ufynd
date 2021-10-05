# ufynd

**Swagger** UI: http://localhost:5000/swagger
**MailHog** UI: http://localhost:8025

# Build instructions

If you have **Docker** installed:
  1. Open the root folder run - docker-compose -f compose.local.yaml up
  2. Run WebApiWithDocker profile

This will add the ability to send the excel report (task 2) by email minutely.
You can find those emails at **MailHog** UI

If you don't have **Docker** installed then simply run WebApi configuration.
You can get the report directly requesting it using **Swagger UI**

# Libraries used

 -- Automapper - object mapping
 -- FluentValidation - functional way to validate requests
 -- NSwag - builing Swagger UI and OpenAPI docs that can be automatically extracted to client calls (axios) implementation
 -- Autofac - IoC for advanced use cases and auto services registration avoiding services.Add{lifetime}() boilerplate
 -- xunit - testing library from NUnit's developer a.k.a NUnit v2
 -- MailHog - ability to send emails to any test address locally avoiding interaction with real SMTP server
 -- ClosedXML - excel file generation without license restrictions on commercial use (EPPlus)
 -- Moq - mocking framework for unit tests
