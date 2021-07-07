using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOWT.Models.EntityDataModels;

namespace WebOWT.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();

        IEnumerable<Category> GetAllCategory();

        IEnumerable<Book> GetAllByUserId(string userName);

        Book GetById(int id);

        void Create(Book book);

        void Update(Book book);

        void Delete(int bookId);
    }

    public class BookService : IBookService
    {
        private OWT_SSEContext _context;

        public BookService(OWT_SSEContext context)
        {
            _context = context;
        }

        public void Create(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int bookId)
        {
            var bookItem = _context.Books.FirstOrDefault(f => f.Id == bookId);
            if (bookItem != null)
            {
                _context.Books.Remove(bookItem);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetAllByUserId(string userName)
        {
            return _context.Books.Where(w => w.Owner.Equals(userName)).ToList();
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.FirstOrDefault(w => w.Id == id);
        }

        public void Update(Book book)
        {
            var item = _context.Books.FirstOrDefault(a => a.Id == book.Id);

            if (item != null)
            {
                item.Title = book.Title;
                item.Description = book.Description;
                item.Author = book.Author;
                item.CoverPhoto = book.CoverPhoto;
                item.CateId = book.CateId;
            }
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
