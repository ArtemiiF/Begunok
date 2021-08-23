using System;
using System.Collections.Generic;
using System.Text;

namespace BegunokApp.DB
{
    public interface IBegunokDBRepository
    {
        int SaveItem(BegunokDB item);
        void DeleteTableItems();
        int DeleteItem(int id);
        BegunokDB GetItem(int id);

        IEnumerable<BegunokDB> GetItems();

    }
}
