using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class StockReportModel
{
    public int StockReportID { get; set; }

    [Required]
    [EnumDataType(typeof(ReportTypeEnum))]
    public ReportTypeEnum ReportType { get; set; }

    [JsonIgnore]
    public List<StockModel> Stocks { get; set; } = new List<StockModel>();

    public StockReportModel(int StockReportID, ReportTypeEnum ReportType, List<StockModel> Stocks)
    {
        this.StockReportID = StockReportID;
        this.ReportType = ReportType;
        this.Stocks = Stocks;
    }

    public StockReportModel()
    {
        Stocks = new List<StockModel>(); // A good practice For required lists, avoids null reference exceptions when adding items.
    }

    public override string ToString()
    {
        return $"[StockReportID: {StockReportID}, ReportType: {ReportType}, Stocks: {Stocks}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is StockReportModel other)
        {
            return StockReportID == other.StockReportID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return StockReportID;
    }

}