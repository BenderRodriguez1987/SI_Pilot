using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Provider
    {

        public string Title { get; set; }
        public List<string> ListProviders { get; set; }
    }
}
