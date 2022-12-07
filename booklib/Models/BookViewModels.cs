﻿using booklib.Entities;
using System.ComponentModel.DataAnnotations;

namespace booklib.Models
{
    public class BookModel
    {
        public Guid BookId { get; set; }

        public string BookName { get; set; }

        public string BookType { get; set; }

        public string BookSubject { get; set; }

        public string Author { get; set; }

        public int Stock { get; set; }

        public string? BookImageFileName { get; set; }

        public DateTime PublishingDate { get; set; }

        public string? Done { get; set; }        
    }

    public class BookSearchModel
    {
        public string BookName { get; set; }

        public string Author { get; set; }

        public string BookImageFileName { get; set; }

    }

    public class BookEditModel
    {
        public Guid BookId { get; set; }

        public string BookName { get; set; }

        public string BookType { get; set; }

        public string BookSubject { get; set; }

        public string Author { get; set; }

        public int Stock { get; set; }
        public string BookImageFileName { get; set; }

        public DateTime PublishingDate { get; set; }
    }   

}
