using API_Development.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_Development.Services
{
    public class BookDictionaryManager : IBookDictionary
    {
        public List<Book> bookList { get; set; }
        public BookDictionaryManager()
        {
            bookList = new List<Book>();
            bookList.Add(new Book { Title = "Starting out with programming logic and design", Description = "To understand the programming fundamentals", Author = "Tony Gaddis", 
                CoverImage = File.ReadAllBytes("Images/Tony.jpg"), Price = 25.99M });
            bookList.Add(new Book { Title = " Programming C# 8.0", Description = "To understand the C# programming language", Author = "Ian Grifths", 
                CoverImage = File.ReadAllBytes("Images/Tony.jpg"), Price = 30.99M });
        }
        public HttpStatusCode Add(Book book)
        {
            if (!isExists(book.Title, book.Author))
            {
                bookList.Add(book);
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.Conflict;
        }

        public HttpStatusCode Delete(string title, string author)
        {
            if(isExists(title,author))
            {
                Book book = bookList.First(x => x.Title == title && x.Author == author);
                bool isDeleted=bookList.Remove(book);
                if (isDeleted)
                    return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        public Book Get(string title, string author)
        {
            Book book=null;
            if (isExists(title, author))
            {
                book = bookList.First(x => x.Title == title && x.Author == author);
            }
            return book;
        }

        public List<Book> GetAll()
        {
            return bookList;
        }
        public HttpStatusCode Update(Book book)
        {
            if (isExists(book.Title, book.Author))
            {
                Book _book = bookList.First(x => x.Title == book.Title && x.Author == book.Author);
                if (book.CoverImage != null)
                {
                    _book.CoverImage = book.CoverImage;
                }
                if (!string.IsNullOrEmpty(book.Description))
                {
                    _book.Description = book.Description;
                }
                if (book.Price > 0)
                {
                    _book.Price = book.Price;
                }
                if (book.CoverImage != null && book.CoverImage.Length>0)
                {
                    _book.CoverImage = book.CoverImage;
                }

                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }
        private bool isExists(string title, string author)
        {
            if (bookList != null && bookList.Count > 0 && bookList.Exists(x => x.Title.ToLower() == title.ToLower() && x.Author.ToLower() == author.ToLower()))
                return true;
            return false;
        }

     
    }

    
}
