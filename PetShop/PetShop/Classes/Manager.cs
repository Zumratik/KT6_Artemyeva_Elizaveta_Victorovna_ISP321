using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PetShop.Classes
{
    class Manager
    {
        public static Frame MainFrame { get; set; }
        public static Data.User CurrentUser { get; set; }
        public static void Image()
        {
            var list = Data.TradeEntities.GetContext().Product.ToList();
            foreach (var i in list)
            {
                string path = Directory.GetCurrentDirectory() + @"/img/" + i.ImageName;
                if (File.Exists(path))
                {
                    i.ImagePath = File.ReadAllBytes(path);
                }

            }
            Data.TradeEntities.GetContext().SaveChanges();
        }
    }
}
