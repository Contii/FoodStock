using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FoodStock.Converters;

namespace FoodStock.Models;

public class ConsumptionReportModel
{
    public int ConsumptionReportID { get; set; }

    [Required]
    private DateTime _reportInitialDate { get; set; }

    [Required]
    [JsonConverter(typeof(JsonDateConverter))]
    public DateTime ReportInitialDate // Ensure only the date part is returned and stored
    { get => _reportInitialDate.Date; set => _reportInitialDate = value.Date; }

    [Required]
    private DateTime _reportFinalDate { get; set; }

    [Required]
    [JsonConverter(typeof(JsonDateConverter))]
    public DateTime ReportFinalDate
    { get => _reportFinalDate.Date; set => _reportFinalDate = value.Date; }

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