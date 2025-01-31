using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class SpoilReportModel
{
    public int SpoilReportID { get; set; }

    [Required]
    [EnumDataType(typeof(ReportTypeEnum))]
    public ReportTypeEnum ReportType { get; set; }

    public List<ItemModel> Itens { get; set; } = new List<ItemModel>(); // Initialize with a default value

    public SpoilReportModel(int spoilReportID, ReportTypeEnum reportType, List<ItemModel> itens)
    {
        SpoilReportID = spoilReportID;
        ReportType = reportType;
        Itens = itens;
    }

    public SpoilReportModel()
    {
    }

    public override string ToString()
    {
        return $"[{SpoilReportID}, {ReportType}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is SpoilReportModel other)
        {
            return other.SpoilReportID == SpoilReportID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return SpoilReportID;
    }

}