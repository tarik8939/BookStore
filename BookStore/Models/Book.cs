using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.Models
{
    public class Book
    {
        
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field 'Name' must be set")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Too long field")]
        [RegularExpression(@"/^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$/", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field 'Author' must be set")]
        [Display(Name = "Author")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Too long field")]
        [RegularExpression(@"/^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$/", ErrorMessage = "Invalid Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The field 'Price' must be set")]
        [Display(Name = "Price")]
        [Range(1, 9999, ErrorMessage = "Too long field")]
        [RegularExpression(@"(?<=(\D|^))[1-9]\d*", ErrorMessage = "Invalid Price")]
        public int Price { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        //public Book()
        //{
        //    Purchases = new List<Purchase>();
        //}
    }
}