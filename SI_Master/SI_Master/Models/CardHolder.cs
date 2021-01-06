using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace SI_Master.Models
{
    public class CardHolder
    {
        [AliasAs("number")]
        public string Number { get; set; }

        [AliasAs("holder")]
        public string Holder { get; set; }
    }
}
