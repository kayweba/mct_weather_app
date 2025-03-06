using StorageService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using StorageService.Database;
using System.Web.Http;
using System.Net;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace StorageService.Web.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class MeasuresController : Microsoft.AspNetCore.Mvc.Controller
    {
        public MeasuresController(MeasureContext context)
        {
            db = context;
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<List<Measure>> Get()
        {
            try
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
                                retValue.morning_precipitation_type = (uint?)measure.Precipitation_typeId;
                                retValue.morning_wind_direction = (uint?)measure.Wind_directionId;
                                break;
                            case 2:
                                retValue.afternoon_temperature = measure.Temperature;
                                retValue.afternoon_pressure = measure.Pressure;
                                retValue.afternoon_wind_speed = measure.Wind_speed;
                                retValue.afternoon_precipitation_type = (uint?)measure.Precipitation_typeId;
                                retValue.afternoon_wind_direction = (uint?)measure.Wind_directionId;
                                break;
                            case 3:
                                retValue.evening_temperature = measure.Temperature;
                                retValue.evening_pressure = measure.Pressure;
                                retValue.evening_wind_speed = measure.Wind_speed;
                                retValue.evening_precipitation_type = (uint?)measure.Precipitation_typeId;
                                retValue.evening_wind_direction = (uint?)measure.Wind_directionId;
                                break;
                            default:
                                throw new DatabaseException("Не удается преобразовать полученное из БД измерение." +
                                    "Неверно указана часть дня.", (int)DbErrorCode.incorrectValue);
                        }
                    }
                    ret.Add(retValue);
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogManager.Instance().Log($"Произошла ошибка при получении измерений. {ex.Message}",
                    MType.Error,
                    MSeverity.Important);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<PMeasure?> Post(PMeasure value)
        {
            try
            {
                if (!PMeasure.Validate(value))
                    // Пришедший JSON некорректен, возвращаем ошибку
                    return UnprocessableEntity(new JsonResult("Invalid value"));
                DbDayPart? dbDayPart = db.dayParts.Find(value.part_of_day);
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(value.date).ToLocalTime();
                long unixTime = ((DateTimeOffset)dateTime.Date).ToUnixTimeSeconds();
                // Ищем сначала в базе такую запись
                DbMeasure? exists = db.measures.Find([unixTime, value.part_of_day]);
                bool shouldBeOverwritten = true;
                if (exists is not null)
                {
                    shouldBeOverwritten = value.force_overwrite.GetValueOrDefault(false);
                }
                // Если такой записи нет или ее надо перезаписать
                if (dbDayPart is not null && shouldBeOverwritten)
                {
                    db.measures.Add(new DbMeasure
                    {
                        Measure_date = (ulong)unixTime,
                        Measure_day_part = value.part_of_day,
                        Day_part = dbDayPart,
                        Temperature = value.temperature,
                        Pressure = value.pressure,
                        Wind_speed = value.wind_speed,
                        Wind_directionId = value.wind_direction,
                        Precipitation_typeId = value.precipitation_type
                    });
                    int written = db.SaveChanges();
                    if (written != 0)
                        return Ok(value);
                    else return Ok(null);
                }
                return Ok(null);
            }
            catch (Exception ex)
            {
                LogManager.Instance().Log($"Произошла ошибка при добавлении измерения. {ex.Message}", 
                    MType.Error,
                    MSeverity.Important);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        private MeasureContext db;
    }
}
