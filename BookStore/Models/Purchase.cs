﻿using System;
using System.Collections.Generic;
namespace BookStore.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string Person { get; set; }
        public string Address { get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public Purchase()
        {
            Books = new List<Book>();
        }
    }
}