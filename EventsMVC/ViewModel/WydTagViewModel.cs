using EventsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsMVC.ViewModel
{
    public class WydTagViewModel
    {

        public TIN_Wydarzenie TIN_Wydarzenie { get; set; }
        public IEnumerable<SelectListItem> AllTags { get; set; }

        private List<decimal> _selectedTags;
        public List<decimal> SelectedTags
        {
            get
            {
                if (_selectedTags == null)
                {
                    _selectedTags = TIN_Wydarzenie.TIN_Tagi.Select(m => m.TIN_Tagi_ID).ToList();
                }
                return _selectedTags;
            }
            set { _selectedTags = value; }
        }
    }
}