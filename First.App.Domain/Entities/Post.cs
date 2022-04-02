using System;
using System.Collections.Generic;
using System.Text;

namespace First.App.Domain.Entities
{
    public class Post : BaseEntity
    {
        public int UserId { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
    }
}
