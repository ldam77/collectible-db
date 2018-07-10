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
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO collectible (item, description) VALUES (@Item, @Description);";
      MySqlParameter item = new MySqlParameter();
      item.ParameterName = "@Item";
      item.Value = this.Item;
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@Description";
      description.Value = this.Description;
      cmd.Parameters.Add(item);
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static Collectible Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM collectible WHERE id=@SearchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@SearchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      string itemName = "";
      string itemDescription = "";
      int itemId = 0;
      while(rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemName = rdr.GetString(1);
        itemDescription = rdr.GetString(2);
      }
      Collectible foundCollectible = new Collectible(itemName, itemDescription, itemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCollectible;
    }
    public static List<Collectible> Find(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM collectible WHERE item=@SearchName;";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@SearchName";
      searchName.Value = name;
      cmd.Parameters.Add(searchName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Collectible> foundList = new List<Collectible> {};
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemName = rdr.GetString(1);
        string itemDescription = rdr.GetString(2);
        Collectible foundCollectible = new Collectible(itemName, itemDescription, itemId);
        foundList.Add(foundCollectible);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundList;
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
