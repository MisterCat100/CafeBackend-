using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;

namespace Server.Controllers;

[Route("CafeAPI")]
[ApiController]
public class CafeController : ControllerBase
{
    private readonly IModelDB _modelDB;

    public CafeController(IModelDB modelDB)
    {
        _modelDB = modelDB;
    }



    [HttpPost]
    [Route("Add")]
    public IActionResult Add([FromBody] Order newOrder)
    {
        _modelDB.AddOrder(newOrder);
        return Ok("Заказ успешно добавлен!!!");
    }


    [HttpPost]
    [Route("Remove")]
    public IActionResult Remove(string name)
    {
        _modelDB.DeleteOrder(name);
        return Ok("Заказ успешно удалён!");
    }


    [HttpGet]
    [Route("Show")]
    public IActionResult Show()
    {
        return Ok(_modelDB.GetAllOrders());
    }

    // TODO: Если админ ввёл нет, то была написана другая надпись, сейчас если ответили НЕТ, то БД не удаляется, но всё равно пишет, что она удалена.
    [HttpPost]
    [Route("EraseDataBase")]
    public IActionResult EraseDataBase(string password)
    {
        if (password == "AkshinIsNice")
        {
            _modelDB.EraseDataBase(password);
            return Ok("База данных ***** удалена, поздравляю!!!");
        }
        return Ok("Неправильно введён пароль");
    }
}
