using FoodStock.Common.Interfaces;
using FoodStock.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FoodStock.Persistence.Observers
{
    public class ConsumptionObserver : IObserver
    {
        private readonly EFCoreContext _context;

        public ConsumptionObserver(EFCoreContext context)
        {
            _context = context;
        }

        public void Update(object newState, float oldQuantity)
        {
            if (newState is ConsumptionModel newConsumption)
            {
                var consumptionReports = _context.ConsumptionReports
                    .Include(cr => cr.Consumptions)
                    .Where(cr => cr.ReportInitialDate <= newConsumption.ConsumptionDate && cr.ReportFinalDate >= newConsumption.ConsumptionDate)
                    .ToList();

                foreach (var report in consumptionReports)
                {
                    if (!report.Consumptions.Contains(newConsumption))
                    {
                        report.Consumptions.Add(newConsumption);
                        _context.Entry(report).State = EntityState.Modified;
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}