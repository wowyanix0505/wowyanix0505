using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prac2.Controllers
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }

    }
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static List<WeatherData> weatherDatas = new()
        {
            new WeatherData() { Id = 1, Date = "21.02.2022", Degree = 10, Location = "Мурманск" },
            new WeatherData() { Id = 23, Date = "10.08.2019", Degree = 20, Location = "Пермь" },
            new WeatherData() { Id = 24, Date = "05.11.2020", Degree = 15, Location = "Омск" },
            new WeatherData() { Id = 25, Date = "07.02.2021", Degree = 0, Location = "Томск" },
            new WeatherData() { Id = 30, Date = "30.05.2022", Degree = 3, Location = "Калининград" },
        };
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<WeatherData> GetAll()
        {
            return weatherDatas;//возвращение всех записей списка
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++)//цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == id)//в случае, если индетификаторы одинаковые - выполним следующее
                    return Ok(weatherDatas[i]);//возвращение результата "Успешно" с данными о записи
            }
            return BadRequest("Такая запись не обнаружена");// возвращение результат "Ошибка" с сообщением
        }
        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            if (data.Id >= 0)
            {
                for (int i = 0; i < weatherDatas.Count; i++)//цикл, который обходит каждый элемент массива weatherDatas
                {
                    if (weatherDatas[i].Id == data.Id)//в случае, если индетификаторы одинаковые - выполним следующее
                        return BadRequest("Запись с таким Id уже есть");//возвращение результата "Ошибка" с сообщением
                }
                weatherDatas.Add(data);//добавляем в список новую запись
                return Ok();//возвращение результата "Успешно"
            }
            else
            {
                return BadRequest("Некорректные данные");//возвращение результата "Ошибка" с сообщением }
            }
        }
        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++)//цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == data.Id)//в случае, если индетификаторы одинаковые - выполним следующее
                {
                    weatherDatas[i] = data;//заменяем значение для данной ячейки массива
                    return Ok();//возвращение результата "Успешно"
                }
            }
            return BadRequest("Такая запись не обнаружена");// возвращение результат "Ошибка" с сообщением
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id >= 0)
            {
                for (int i = 0; i < weatherDatas.Count; i++)//цикл, который обходит каждый элемент массива weatherDatas
                {
                    if (weatherDatas[i].Id == id)//в случае, если индетификаторы одинаковые - выполним следующее
                    {
                        weatherDatas.RemoveAt(i);//удаляем элемент из массива по его индексу (переменная i)
                        return Ok();//возвращение результата "Успешно"
                    }
                }
                return BadRequest("Такая запись не обнаружена");// возвращение результат "Ошибка" с сообщением

            }
            else
            {
                return BadRequest("Некорректные данные");//возвращение результата "Ошибка" с сообщением
            }
        }
        [HttpGet("find-by-city")]
        public IActionResult GetByCityName(string location)
        {
            for (int i = 0; i < weatherDatas.Count; i++)//цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Location == location)//в случае, если локация одинаковая - выполним следующее
                    return Ok("Запись с указанным городом имеется в нашем списке");//возвращение результата "Успешно" с сообщением
            }
            return BadRequest("Запись с указанным городом не обнаружено");// возвращение результат "Ошибка" с сообщением
        }
    }
}