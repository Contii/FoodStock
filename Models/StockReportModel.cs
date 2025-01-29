using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class StockReportModel
{
    public int StockReportID { get; set; }

    [Required]
    [EnumDataType(typeof(ReportTypeEnum))]
    public ReportTypeEnum ReportType { get; set; }

    public List<StockModel> Stocks { get; set; } = new List<StockModel>(); // Initialize with a default value

    public StockReportModel(int stockReportID, ReportTypeEnum reportType, List<StockModel> stocks)
    {
        StockReportID = stockReportID;
        ReportType = reportType;
        Stocks = stocks;
    }

    public StockReportModel()
    {
    }

    public override string ToString()
    {
        return $"[{StockReportID}, {ReportType}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is StockReportModel other)
        {
            return other.StockReportID == StockReportID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return StockReportID;
    }

}