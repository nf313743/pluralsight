using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WiredBrainCoffee.Models;
using WiredBrainCoffee.Services;

namespace WiredBrainCoffee.Pages
{
    public class MenuModel : PageModel
    {
        private readonly ILogger<MenuModel> _logger;
        private readonly IMenuService _menuService;

        public MenuModel(ILogger<MenuModel> logger, IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        public List<MenuItem> Menu { get; set; }

        public void OnGet()
        {
            try
            {
                Menu = _menuService.GetMenuItems();
                throw new Exception();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not retrieve menu");

            }
        }
    }
}