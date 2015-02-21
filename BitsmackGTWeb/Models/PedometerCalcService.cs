using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitsmackGTWeb.Models
{
    public class PedometerCalcService
    {
        private List<Pedometer> _pedometerRecords;

        public List<Pedometer> PedommeterRecords
        {
            get {
                return _pedometerRecords ??
                       (_pedometerRecords = WebAPIClient.Get<List<Pedometer>>(Constants.ApiPedometer));
            }
        } 

        public Dictionary<DateTime, double> MonthStepPercentages()
        {
            var records = MonthPedometer();

            var PR = records.Max(x => x.steps);
            var list = new Dictionary<DateTime, double>();
            foreach (var d in records)
            {
                var pct = (double)d.steps/PR;
                list.Add(d.trandate,pct);
            }
            return list;
        }

        public List<Pedometer> MonthPedometer()
        {
            try
            {
                var listRecs =
                    PedommeterRecords.GroupBy(x => new {x.trandate.Year, x.trandate.Month}).Select(y => new Pedometer
                        {
                            //bodyfat = y.Average(z => z.bodyfat),
                            //calconsumed = (int?) y.Where(z => z.calconsumed > 1000).Average(z => z.calconsumed),
                            //sleep = (int) y.Where(z => z.sleep > 0).Average(z => z.sleep),
                            steps = y.Sum(z => z.steps),
                            //weight = y.Where(z => z.weight > 0).Average(z => z.weight),
                            trandate = new DateTime(y.Key.Year, y.Key.Month, 1)
                        });
                return listRecs.OrderBy(x => x.trandate).ToList();
            }
            catch (Exception ex)
            {
                return new List<Pedometer>();
            }
        }

        public int StepsPR()
        {
            return MonthPedometer().Max(x => x.steps);
        }

        public double MonthPct(int month, int year)
        {
            var monthSteps = MonthPedometer().FirstOrDefault(x => x.trandate.Year == year && x.trandate.Month == month);
            return monthSteps != null ? (double) monthSteps.steps/StepsPR() : 0;
        }

        public double MonthPctExpected(DateTime now)
        {
            var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            return (double) now.Day/daysInMonth;
        }

        public double GetStartWeight(int year)
        {
            var firstYearRec = PedommeterRecords.Where(x => x.trandate.Year == year).OrderBy(x => x.trandate).FirstOrDefault();
            return firstYearRec != null ? firstYearRec.weight : 0;
        }

        public double GetRecentWeight()
        {
            var recentRec =
                PedommeterRecords.Where(x => x.weight > 0).OrderByDescending(x => x.trandate).FirstOrDefault();
            return recentRec != null ? recentRec.weight : 0;
        }
    }
}