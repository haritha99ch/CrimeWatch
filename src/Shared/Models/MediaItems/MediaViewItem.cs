using Domain.AggregateModels.ReportAggregate.Enums;

namespace Shared.Models.MediaItems;
public record MediaViewItem(string Url, MediaType MediaType) : ISelector;
