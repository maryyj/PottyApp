﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PottyAppNew.Models
{
    public class Event
    {
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public string EventDescription { get; set; }
        public ObjectId ChildId { get; set; }
        public Guid ParentId { get; set; }
    }
}
