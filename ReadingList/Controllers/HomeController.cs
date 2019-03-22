using ReadingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadingList.Controllers
{
    public class HomeController : Controller
    {
        private static IBookManager bookManagerCached;


        public HomeController(IBookManager bookManager)
        {
            if (HomeController.bookManagerCached == null)
            {
                HomeController.bookManagerCached = bookManager;
            }
        }

        public ActionResult Index()
        {
            List<Book> shelvedBooks = HomeController.bookManagerCached.GetShelvedBooks();

            ViewData["ShelvedBooks"] = shelvedBooks;
            ViewData["Title"] = "MyShelf";

            return View(shelvedBooks);
        }

        public ActionResult Book(long id)
        {
            Book shelvedBook = HomeController.bookManagerCached.GetShelvedBook(id);

            ViewData["ShelvedBook"] = shelvedBook;
            ViewData["Title"] = "My Shelved Book";

            return View(shelvedBook);
        }

        public ActionResult RateBooks()
        {
            Book neutralBook = HomeController.bookManagerCached.GetNeutralBook();

            ViewData["NeutralBook"] = neutralBook;
            ViewData["Title"] = "RateBooks";

            return View(neutralBook);
        }

        public ActionResult StoreBook(long currentBookId, bool isSaved)
        {
            if (isSaved)
            {
                HomeController.bookManagerCached.AddShelvedBook(currentBookId);
            }
            else
            {
                HomeController.bookManagerCached.AddRejectedBook(currentBookId);
            }
            HomeController.bookManagerCached.RemoveNeutralBook(currentBookId);
            return Redirect("/Home/RateBooks/");
        }

        public ActionResult RemoveBookFromShelf(long id)
        {
            HomeController.bookManagerCached.RemoveShelvedBook(id);
            return Redirect("/Home/Index");
        }

        // Remove all books from shelf and place all books back under neutral books
        public ActionResult ResetAllBooks()
        {
            HomeController.bookManagerCached.ResetAllBooks();
            return Redirect("/Home/RateBooks/");
        }

        public ActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return null;
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}