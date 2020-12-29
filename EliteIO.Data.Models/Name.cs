using MongoDB.Bson;

namespace EliteIO.Data.Models
{
    public class Name
    {
        public Name()
        {
            _id = ObjectId.GenerateNewId();
            _t = "Name";
            NameVal = string.Empty;
        }

        public ObjectId _id { get; set; }
        public string _t { get; set; }
        public string NameVal { get; set; }
    }
}