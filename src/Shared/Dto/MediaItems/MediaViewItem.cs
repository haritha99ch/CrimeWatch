using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Shared.Dto.MediaItems;
public record MediaViewItem(EvidenceId EvidenceId, string Url, MediaType MediaType);
