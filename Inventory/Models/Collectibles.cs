using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Inventory;

namespace Inventory.Models
{
  public class Collectible
  {
    private string Item;
    private string Description;
    private int Id;

    public Collectible(string newItem = "", string newDescription = "", int newId = 0)
    {
      Id = newId;
      Item = newItem;
      Description = newDescription;
    }

    public int GetId()
    {
      return Id;
    }
    public string GetItem()
    {
      return Item;
    }
    public string GetDescription()
    {
      return Description;
    }
    public static List<Collectible> GetAll()
    {
      List<Collectible> allCollectibles = new List<Collectible> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM collectible;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemName = rdr.GetString(1);
        string itemDescription = rdr.GetString(2);
        Collectible newCollectible = new Collectible(itemName, itemDescription, itemId);
        allCollectibles.Add(newCollectible);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCollectibles;
    }
    public override bool Equals(System.Object otherCollectible)
    {
      if (!(otherCollectible is Collectible))
      {
        return false;
      }
      else
      {
        Collectible newCollectible = (Collectible) otherCollectible;
        bool nameEquality = (this.GetItem() == newCollectible.GetItem());
        bool descriptionEquality = (this.GetDescription() == newCollectible.GetDescription());
        bool idEquality = (this.GetId() == newCollectible.GetId());
        return (nameEquality && descriptionEquality && idEquality);
      }
    }
    public static void DeleteAll()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM collectible;";

     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }
  }
}
