using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustPlay.Data.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }

        public string AuthId { get; set; }
       // public List<int> VideogamesOwned { get; set; }
        //public List<int> VideogamesWishlist { get; set; }

        public override bool Equals(object obj)
        {
            var objectToCompare = obj as User;

            if (objectToCompare == null)
            {
                return false;
            }
            if (objectToCompare.Email == this.Email &&
                objectToCompare.Admin == this.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static implicit operator User(ActionResult<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
