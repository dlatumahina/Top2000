using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Top2000GOED.Models;

namespace Top2000GOED.ViewModels
{
    public class ArtiestDetailsViewModel
    {
        public Artiest Artiest { get; set; }

        public List<Song> Songs { get; set; }

    }
}