using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Client> allClients = Client.GetAll();
      List<Stylist> allStylists = Stylist.GetAll();

      model.Add("client",allClients);
      model.Add("stylist",allStylists);

      return View("Index",model);
    }

    [HttpGet("/stylist/add")]
    public ActionResult AddStylist()
    {
      return View();
    }

    [HttpPost("/stylist/list")]
    public ActionResult WriteStylistList()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();

      return View("ViewStylists", allStylists);
    }

    [HttpGet("/stylist/list")]
    public ActionResult ReadStylistList()
    {
      List<Stylist> allStylists = Stylist.GetAll();

      return View("ViewStylists", allStylists);
    }

    [HttpGet("/{name}/{id}/clientlist")]
    public ActionResult ViewClientList(int id)
    {

      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id); //Stylist is selected as an object
      List<Client> stylistClients = selectedStylist.GetClients(); //Clients are displayed in a list

      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);

      return View("stylistClients", model);
    }

    [HttpGet("/{name}/{id}/client/add")]
    public ActionResult AddClient(int id)
    {
      Stylist selectedStylist = Stylist.Find(id); //Stylist is selected as an object

      return View(selectedStylist);
    }

    [HttpPost("/{name}/{id}/clientlist")]
    public ActionResult AddClientViewClientList(int id)
    {
      Client newClient = new Client(Request.Form["client-name"], id);
      newClient.Save();
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id); //Stylist is selected as an object
      List<Client> stylistClients = selectedStylist.GetClients(); //Clients are displayed in a list

      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);

      //return the client list for selected stylist
      return View("stylistClients", model);
    }

    //client id
    [HttpGet("/clients/{id}/edit")]
    public ActionResult ClientEdit(int id)
    {
      Client thisClient = Client.Find(id);
      return View(thisClient);
    }

    [HttpPost("/clients/{id}/edit")]
    public ActionResult ClientEditConfirm(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.UpdateClientName(Request.Form["new-name"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult ClientDeleteConfirm(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.DeleteClient();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/edit")]
    public ActionResult StylistEdit(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }

    [HttpPost("/stylists/{id}/edit")]
    public ActionResult StylistEditConfirm(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateStylistName(Request.Form["stylist-name"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/delete")]
    public ActionResult StylistDeleteConfirm(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.DeleteStylist();
      // Client thisClient = Client.Find(id);
      // thisClient.DeleteClient();
      return RedirectToAction("Index");
    }

    [HttpGet("/Client/list")]
    public ActionResult AllClients()
    {
      List<Client> allClients = Client.GetAll();
      return View("ViewClients", allClients);
    }
  }
}
