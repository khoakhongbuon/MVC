using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using W2_test.DataAccess;
using W2_test.Models;

namespace W2_test.Controllers
{
	public class HomeController : Controller
	{
		private readonly BlogDAL _blogDAL;

		public HomeController(IConfiguration configuration)
		{
			_blogDAL = new BlogDAL(configuration);
		}

		//  danh sách blog
		public IActionResult Index()
		{
			var blogs = _blogDAL.GetAllBlogs();
			return View(blogs);
		}

		// Xem Blog Id
		public IActionResult Details(int id)
		{
			var blog = _blogDAL.GetBlogById(id);
			if (blog == null)
			{
				return NotFound();
			}
			return View(blog);
		}
	}
}
