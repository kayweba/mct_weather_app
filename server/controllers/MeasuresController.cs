using StorageService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using StorageService.Database;

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
            List<DbMeasure> measures = db.measures.ToList();
            //TODO �������� ��� ��������� � ����������� ������ � ��������� ������������ ��������
            return new List<Measure>
            {
                new Measure
                {
                    date = 1739207223,
                    morning_temperature = 10,
                    morning_pressure = 747,
                    morning_wind_speed = 3,
                    morning_wind_direction = 0,
                    morning_precipitation_type = 0,
                    afternoon_temperature = 15,
                    afternoon_pressure = 750,
                    afternoon_wind_speed = 2,
                    afternoon_wind_direction = 1,
                    afternoon_precipitation_type = 1,
                    evening_temperature = 11,
                    evening_pressure = 751,
                    evening_wind_speed = 6,
                    evening_wind_direction = 2,
                    evening_precipitation_type = 1
                },
                new Measure
                {
                    date = 1739293623,
                    morning_temperature = 15,
                    morning_pressure = 743,
                    morning_wind_speed = 2,
                    morning_wind_direction = 3,
                    morning_precipitation_type = 3,
                    afternoon_temperature = 18,
                    afternoon_pressure = 755,
                    afternoon_wind_speed = 7,
                    afternoon_wind_direction = 4,
                    afternoon_precipitation_type = 4,
                    evening_temperature = 14,
                    evening_pressure = 756,
                    evening_wind_speed = 4,
                    evening_wind_direction = 5,
                    evening_precipitation_type = 5
                },
                new Measure
                {
                    date = 1739380023,
                    morning_temperature = 15,
                    morning_pressure = 743,
                    morning_wind_speed = 2,
                    morning_wind_direction = 6,
                    morning_precipitation_type = 0,
                    afternoon_temperature = 3,
                    afternoon_pressure = 755,
                    afternoon_wind_speed = 7,
                    afternoon_wind_direction = 7,
                    afternoon_precipitation_type = 1,
                    evening_temperature = 10,
                    evening_pressure = 756,
                    evening_wind_speed = 4,
                    evening_wind_direction = 0,
                    evening_precipitation_type = 2
                }
            };
        }
        private MeasureContext db;
    }
}
