using System.Collections.Generic;

namespace FluentDataAnnotations
{
    using System.Web.Mvc;

    public class DropDownProperties
    {
        public IEnumerable<SelectListItem> SelectList { get; set; }

        public string OptionLabel { get; set; }
    }
}
