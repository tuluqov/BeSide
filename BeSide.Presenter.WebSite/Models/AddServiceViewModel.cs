using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Models
{
    public class AddServiceViewModel
    {
        public string ServiceName { get; set; }

        public int CategoryId { get; set; }
    }
}