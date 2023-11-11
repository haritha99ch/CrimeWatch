using System.ComponentModel.DataAnnotations;

namespace Domain.AggregateModels.ReportAggregate.Enums;
public enum ViolationType
{
    Traffic = 0,
    Assault = 1,
    Theft = 2,
    Fraud = 3,
    [Display(Name = "Drug Abuse")]
    DrugAbuse = 4,
    DomesticViolence = 5,
    CyberCrime = 6,
    Murder = 8,
    CivilRights = 9
}
