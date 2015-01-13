using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentDataAnnotations
{
    public class ValueTransform
    {
        public Func<string,string> ValueTransformFunc { get; set; }

        public bool ApplyTransformInEditMode { get; set; }
    }
}
