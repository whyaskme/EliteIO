using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MongoDB.Driver;

using EliteIO.Data.Models;

namespace EliteIO.Apps.Website.Pages.Forms
{
    public class AddAddressModel : PageModel
    {
        [BindProperty]
        public string StreetAddress1 { get; set; }

        [BindProperty]
        public string StreetAddress2 { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string State { get; set; }

        [BindProperty]
        public string ZipCode { get; set; }

        IMongoCollection<User> _mongoUserCollection;

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                try
                {
                    //bool _useLocalHostDB = Convert.ToBoolean(ConfigurationManager.AppSettings["UseLocalHost"]);
                    //if (_useLocalHostDB)
                    //{
                    //    _dbConnectionString = ConfigurationManager.ConnectionStrings["LocalMongoServer"].ConnectionString;
                    //    _mongoClient = new MongoClient(_dbConnectionString);
                    //    _mongoDatabase = _mongoClient.GetDatabase(ConfigurationManager.AppSettings["LocalMongoDbName"]);
                    //}
                    //else
                    //{
                    //    _dbConnectionString = ConfigurationManager.ConnectionStrings["MongoServer"].ConnectionString;
                    //    _mongoClient = new MongoClient(_dbConnectionString);
                    //    _mongoDatabase = _mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbName"]);
                    //}

                    string connStringAzure = @"mongodb://eliteio-mongodb:C1nzL1jetKTlEAb1kelNRU6a3pxoE5Xl0CRdgVk9UiQb0vMD9z5LpPHea2yDEJMZGfbrosYau5fOnXhKcRPbhA==@eliteio-mongodb.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@eliteio-mongodb@";

                    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connStringAzure));

                    settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                    var mongoAzureClient = new MongoClient(settings);
                    var dbAzureReferences = mongoAzureClient.GetDatabase("References");

                    //dbAzureReferences.CreateCollection("Test-Coll");
                    _mongoUserCollection = dbAzureReferences.GetCollection<User>("User");

                    //List<User> _userList = _mongoUserCollection.Find<User>(s => s.Enabled == true).SortBy(s => s.Name).ToListAsync<User>().Result;
                    //List<User> _users = _mongoUserCollection.Find<User>(s => s.Contact.Email.UserName == userName && s.Contact.Email.Domain == userDomain).ToListAsync<User>().Result;
                    //foreach (User _user in _users)
                    //{
                    //    return _user;
                    //}

                    List<User> _userList = _mongoUserCollection.Find<User>(s => s.Enabled == true).ToListAsync<User>().Result;

                    //_mongoUserCollection.InsertOneAsync
                    //_mongoUserCollection.UpdateOneAsync

                    User _user = new Data.Models.User();
                    _user.FirstName = "Joe";
                    _user.LastName = "Baranauskas";
                    _user.Contact.Address.Address1 = "123 Some Street";

                    _mongoUserCollection.InsertOne(_user);

                }
                catch (Exception ex)
                {
                    var errMsg = ex.Message;
                }

                return RedirectToPage("../Index");
            }
        }
    }
}
