using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Comp4945_MVC_Assignment.Models
{
    [MetadataType(typeof(customerMetadata))]
    public partial class customer
    {
        public class customerMetadata
        {
            [DisplayName("CustomerName")]
            public string custname { get; set; }
            [DisplayName("Address")]
            public string address { get; set; }
            [DisplayName("Mobile")]
            public Nullable<int> mobile { get; set; }

        }
    }
}