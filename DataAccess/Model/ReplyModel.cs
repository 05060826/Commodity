using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class ReplyModel
    {
        public string Isbn { get; set; }
        public List<string> ReplyA { get; set; }
    }
}
