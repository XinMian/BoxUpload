using ApplicationCore.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public partial class BoxContext
    {
        public BoxContext(DbContextOptionBuilder option) : base(option.Build())
        {

        }
    }
}
