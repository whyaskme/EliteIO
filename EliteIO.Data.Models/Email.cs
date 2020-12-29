using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EliteIO.Data.Models
{
    public class Email
    {
        public Email()
        {
            UserName = "user";
            Domain = "@some-domain.com";
        }
        public string UserName { get; set; }
        public string Domain { get; set; }
    }
}