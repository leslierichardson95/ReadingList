using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Models
{
    public interface IBookManager
    {
        List<Book> GetNeutralBooks();

        List<Book> GetShelvedBooks();

        List<Book> GetRejectedBooks();

        Book GetNeutralBook();

        Book GetShelvedBook(long id);

        void AddShelvedBook(long id);

        void AddRejectedBook(long id);

        void AddNeutralBook(Book book);

        void RemoveNeutralBook(long id);

        void RemoveShelvedBook(long id);

        void RemoveRejectedBook(long id);

        void ResetAllBooks();

        string GetBase64StringForImage(string imgPath);
    }
}
