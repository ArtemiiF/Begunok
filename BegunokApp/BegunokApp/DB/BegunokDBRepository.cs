using System;
using System.Collections.Generic;
using BegunokApp.Models;
using System.Text;
using SQLite;

namespace BegunokApp.DB
{
    public class BegunokDBRepository : IBegunokDBRepository
    {
        SQLiteConnection database;

        public BegunokDBRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<BegunokDB>();
        }

        public int SaveItem(BegunokDB item)
        {
            if (item.Id != 0)
            {
                System.Diagnostics.Debug.WriteLine($"DB Id:{item.Id} updated");
                database.Update(item);
                return item.Id;
            }

            return database.Insert(item);
        }

        public void RefreshTable()
        {
            try
            {
                database.DropTable<BegunokDB>();
                database.CreateTable<BegunokDB>();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }           
        }

        public int DeleteItem(int id)
        {
            return database.Delete<BegunokDB>(id);
        }

        public BegunokDB GetItem(int id)
        {
            return database.Get<BegunokDB>(id);
        }

        public IEnumerable<BegunokDB> GetItems()
        {
            return database.Table<BegunokDB>().ToList();
        }
    }
}
