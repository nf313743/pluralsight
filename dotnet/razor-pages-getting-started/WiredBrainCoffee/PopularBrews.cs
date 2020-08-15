using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffee.Services;

namespace WiredBrainCoffee
{
    public class PopularBrews : ViewComponent
    {
        private IMenuService _menuService;

        public PopularBrews(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public Task<IViewComponentResult> InvokeAsync(int count)
        {
            var result = _menuService.GetMenuItems().Take(count);
            return Task.FromResult<IViewComponentResult>(View(result));
        }

    }
}