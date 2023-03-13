using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PottyAppNew.Models
{
    public class Child
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Points { get; set; }
        public Guid ParentId { get; set; }
    }
}
