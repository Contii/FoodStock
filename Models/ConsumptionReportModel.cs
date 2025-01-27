using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class ConsumptionReportModel
{
    public int ConsumptionReportID { get; set; }

    [Required]
    public DateTime ReportInitialDate { get; set; }

    [Required]
    public DateTime ReportFinalDate { get; set; }

    [JsonIgnore]
    public List<ConsumptionModel> Consumptions { get; set; } = new List<ConsumptionModel>(); // Initialize with a default value

    public ConsumptionReportModel(int consumptionReportID, DateTime reportInitialDate, DateTime reportFinalDate, List<ConsumptionModel> consumptions)
    {
        ConsumptionReportID = consumptionReportID;
        ReportInitialDate = reportInitialDate;
        ReportFinalDate = reportFinalDate;
        Consumptions = consumptions;
    }

    public ConsumptionReportModel()
    {
    }

    public override string ToString()
    {
        return $"[{ConsumptionReportID}, {ReportInitialDate}, {ReportFinalDate}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is ConsumptionReportModel other)
        {
            return other.ConsumptionReportID == ConsumptionReportID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ConsumptionReportID;
    }

}