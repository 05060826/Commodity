using System;
using System.Collections.Generic;

namespace DBfirst.dbs
{
    public partial class StudentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int? ClassId { get; set; }
    }
}
