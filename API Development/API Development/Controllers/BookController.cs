using API_Development.Models;
using API_Development.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Development.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookDictionary _bookManager;
        public BookController(IBookDictionary bookDictionary)
        {
            _bookManager = bookDictionary;
        }
        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Book> bookList = _bookManager.GetAll();
            if (bookList != null && bookList.Count > 0)
            {
                return Ok(bookList);
            }
            return NoContent();
        }

        // GET api/<BookController>/5
        [HttpGet("{title}&{author}")]
        public IActionResult Get(string title, string author)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
            {
                Book book = _bookManager.Get(title, author);
                if (book != null)
                    return Ok(book);
                return NotFound("Book with given title and author names doesn't found in the list");
            }
            else
            {
                return BadRequest("Title or Author name has not been provided.");
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if(book!=null && !string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Author))
            {
                HttpStatusCode isAddedCode = _bookManager.Add(book);

                if(isAddedCode==HttpStatusCode.OK)
                {
                    return Ok("Book has successfully been added.");
                }
                return Conflict("Book with either same name or title already exists.");
            }
            return BadRequest("Given book is either null or doesn't contains title or author name.");
        }

        // PUT api/<BookController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if(book != null && !string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Author))
            {
                HttpStatusCode isDeletedCode = _bookManager.Update(book);

                if (isDeletedCode == HttpStatusCode.OK)
                    return Ok("Book has been updated successfully.");
                else
                    return NotFound("Book with given name and author doesn't found in the list");

            }
            return BadRequest("Given book is either null or doesn't contains title or author name.");
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{title}&{author}")]
        public IActionResult Delete(string title, string author)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
            {
                HttpStatusCode isDeletedCode = _bookManager.Delete(title, author);
                if (isDeletedCode == HttpStatusCode.OK)
                    return Ok("Book has been deleted successfully.");
                else
                    return NotFound("Book with provided title and author doesn't exist.");
            }
            else
            {
                return BadRequest("Title or Author name has not been provided.");
            }
        }
    }
}
