using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FullTextSearch.Models
{
    public class WikiReference
    {
        public Guid Id { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(512)]
        public string Url { get; set; }

        public string Content { get; set; }
    }
}