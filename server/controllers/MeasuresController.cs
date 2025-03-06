using StorageService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using StorageService.Database;
using System.Data.Common;
using System.Linq;

namespace StorageService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasuresController : Microsoft.AspNetCore.Mvc.Controller
    {
        public MeasuresController(MeasureContext context)
        {
            db = context;
        }
        [HttpGet]
        public List<Measure> Get()
        {
            List<Measure> ret = new List<Measure>();
            List<DbMeasure> measures = db.measures.ToList();
            Dictionary<ulong, List<DbMeasure>> unitedMeasures = new Dictionary<ulong, List<DbMeasure>>();
            foreach (DbMeasure measure in measures)
            {
                if (!unitedMeasures.ContainsKey(measure.Measure_date))
                    unitedMeasures.Add(measure.Measure_date, new List<DbMeasure>());
                unitedMeasures[measure.Measure_date].Add(measure);
            }
            foreach (var dateMeasure in unitedMeasures)
            {
                Measure retValue = new Measure { date = dateMeasure.Key };
                foreach (DbMeasure measure in dateMeasure.Value)
                {
                    switch (measure.Measure_day_part)
                    {
                        case 1:
                            retValue.morning_temperature = measure.Temperature;
                            retValue.morning_pressure = measure.Pressure;
                            retValue.morning_wind_speed = measure.Wind_speed;
                            retValue.morning_precipitation_type = (uint) measure.Precipitation_typeId;
                            retValue.morning_wind_direction = (uint) measure.Wind_directionId;
                            break;
                        case 2:
                            retValue.afternoon_temperature = measure.Temperature;
                            retValue.afternoon_pressure = measure.Pressure;
                            retValue.afternoon_wind_speed = measure.Wind_speed;
                            retValue.afternoon_precipitation_type = (uint) measure.Precipitation_typeId;
                            retValue.afternoon_wind_direction = (uint) measure.Wind_directionId;
                            break;
                        case 3:
                            retValue.evening_temperature = measure.Temperature;
                            retValue.evening_pressure = measure.Pressure;
                            retValue.evening_wind_speed = measure.Wind_speed;
                            retValue.evening_precipitation_type = (uint)measure.Precipitation_typeId;
                            retValue.evening_wind_direction = (uint)measure.Wind_directionId;
                            break;
                        default:
                            throw new DatabaseException("Не удается преобразовать полученное из БД измерение." +
                                "Неверно указана часть дня.", (int) DbErrorCode.incorrectValue);
                    }
                }
                ret.Add(retValue);
            }
            return ret;
        }
        private MeasureContext db;
    }
}
