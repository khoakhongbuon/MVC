using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using W2_test.DataAccess;
using W2_test.Models;

public class AdminController : Controller
{
	private readonly BlogDAL _blogDAL;

	public AdminController(IConfiguration configuration)
	{
		_blogDAL = new BlogDAL(configuration);
	}

	public IActionResult Index()
	{
		var blogs = _blogDAL.GetAllBlogs();
		return View(blogs);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Create(Blog blog)
	{
		if (ModelState.IsValid)
		{
			_blogDAL.AddBlog(blog);
			return RedirectToAction("Index");
		}
		return View(blog);
	}

	public IActionResult Edit(int id)
	{
		var blog = _blogDAL.GetBlogById(id);
		if (blog == null)
		{
			return NotFound();
		}
		return View(blog);
	}

	[HttpPost]
	public IActionResult Edit(Blog blog)
	{
		if (ModelState.IsValid)
		{
			_blogDAL.UpdateBlog(blog);
			return RedirectToAction("Index");
		}
		return View(blog);
	}

	public IActionResult Delete(int id)
	{
		var blog = _blogDAL.GetBlogById(id);
		return View(blog);
	}

	[HttpPost, ActionName("Delete")]
	public IActionResult DeleteConfirmed(int id)
	{
		_blogDAL.DeleteBlog(id);
		return RedirectToAction("Index");
	}
}
