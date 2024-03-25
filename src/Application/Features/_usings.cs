﻿global using Application.Common.Results;
global using Application.Contracts.Commands;
global using Application.Contracts.Queries;
global using Application.Errors.Accounts;
global using Application.Errors.Common;
global using Application.Errors.Reports;
global using Domain.AggregateModels.AccountAggregate;
global using Domain.AggregateModels.AccountAggregate.Enums;
global using Domain.AggregateModels.AccountAggregate.ValueObjects;
global using Domain.AggregateModels.ReportAggregate;
global using Domain.AggregateModels.ReportAggregate.Enums;
global using Domain.Common.Models;
global using MediatR;
global using Persistence.Contracts.Repositories;
global using Domain.AggregateModels.ReportAggregate.ValueObjects;
global using Application.Contracts.Services;
global using Application.Selectors.Reports;
global using Application.Selectors.Accounts;
global using Persistence.Helpers.Selectors;
