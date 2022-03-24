using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Bc.Common.Data
namespace Bc
{
    /// <summary>
    /// Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionPropertyAttribute : Attribute
    {
        string description = null;

        public DescriptionPropertyAttribute(string value)
        {
            this.Description = value;
        }

        /// <summary>
        /// Get and set.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
