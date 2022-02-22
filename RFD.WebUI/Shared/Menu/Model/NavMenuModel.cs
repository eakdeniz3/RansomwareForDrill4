using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Shared.Menu.Model
{
    public class NavMenuModel
    {
        public string Title { get; set; }
        public List<Menu> Menu { get; set; }

        public NavMenuModel()
        {
            Menu = new List<Menu>();
        }

    }

    public class Menu
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public List<Menu> SubMenu { get; set; } 

        public bool IsActive { get; set; } 

        public Menu()
        {
            SubMenu = new List<Menu>();
        }
    }

}
