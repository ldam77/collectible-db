using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Inventory.Models;
using MySql.Data.MySqlClient;
using System;

namespace Inventory.Controllers
{
  public class CollectiblesController : Controller
  {
    [HttpGet("/collectibles")]
    public ActionResult Index()
    {
      List<Collectible> allCollectibles = Collectible.GetAll();
      return View(allCollectibles);
    }
    [HttpGet("/collectibles/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/collectibles")]
    public ActionResult Create(string newItemName, string newItemDescription)
    {
      Collectible newCollectible = new Collectible(newItemName, newItemDescription);
      newCollectible.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/collectibles/search")]
    public ActionResult Find()
    {
      return View();
    }
    [HttpPost("/collectibles/search")]
    public ActionResult Search(string newItemId, string newItemName)
    {
      List<Collectible> foundList = new List<Collectible> {};
      if(!string.IsNullOrWhiteSpace(Request.Form["newItemName"]))
      {
        foundList = Collectible.Find(newItemName);
      }
      else if(!string.IsNullOrWhiteSpace(Request.Form["newItemId"]) && int.Parse(newItemId)>0)
      {
        int searchId = int.Parse(newItemId);
        Collectible foundCollectible = Collectible.Find(searchId);
        foundList.Add(foundCollectible);
      }
      else
      {
        return View("Find");
      }
      return View("SearchResult", foundList);
    }
  }
}
