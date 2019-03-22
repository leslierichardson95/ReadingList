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
        private readonly IBookManager bookManager;


        public HomeController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }

        public ActionResult Index()
        {
            List<Book> shelvedBooks = bookManager.GetShelvedBooks();

            ViewData["ShelvedBooks"] = shelvedBooks;
            ViewData["Title"] = "MyShelf";

            return View(shelvedBooks);
        }

        public ActionResult Book(long id)
        {
            Book shelvedBook = bookManager.GetShelvedBook(id);

            ViewData["ShelvedBook"] = shelvedBook;
            ViewData["Title"] = "My Shelved Book";

            return View(shelvedBook);
        }

        public ActionResult RateBooks()
        {
            Book neutralBook = bookManager.GetNeutralBook();

            ViewData["NeutralBook"] = neutralBook;
            ViewData["Title"] = "RateBooks";

            return View(neutralBook);
        }

        public ActionResult StoreBook(long currentBookId, bool isSaved)
        {
            if (isSaved)
            {
                bookManager.AddShelvedBook(currentBookId);
            }
            else
            {
                bookManager.AddRejectedBook(currentBookId);
            }
            bookManager.RemoveNeutralBook(currentBookId);
            return Redirect("/Home/RateBooks/");
        }

        public ActionResult RemoveBookFromShelf(long id)
        {
            bookManager.RemoveShelvedBook(id);
            return Redirect("/Home/Index");
        }

        // Remove all books from shelf and place all books back under neutral books
        public ActionResult ResetAllBooks()
        {
            bookManager.ResetAllBooks();
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