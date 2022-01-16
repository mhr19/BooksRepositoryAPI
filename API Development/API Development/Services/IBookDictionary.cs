using API_Development.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_Development.Services
{
    public interface IBookDictionary
    {
        List<Book> bookList { get; set; }
        HttpStatusCode Add(Book book);
        Book Get(string title, string author);
        List<Book> GetAll();
        HttpStatusCode Delete(string title, string author);
        HttpStatusCode Update(Book book);

    }
}
