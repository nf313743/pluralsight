using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffee.Services;

namespace WiredBrainCoffee
{
    public class PopularBrews : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(int count)
        {
            var menu = new MenuService();
            var result = menu.GetMenuItems().Take(count);
            return Task.FromResult<IViewComponentResult>(View(result));
        }

    }
}