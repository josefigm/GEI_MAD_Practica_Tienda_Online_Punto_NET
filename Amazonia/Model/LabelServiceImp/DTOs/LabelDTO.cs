using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs
{
    /// <summary>
    ///     This class represents a Label without it's comment list.
    /// </summary>
    [Serializable()]
    public class LabelDTO
    {
        public long id { get; set; }

        public LabelDTO(long id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as LabelDTO;
            return dTO != null &&
                   id == dTO.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}
