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
        public string value { get; set; }

        public LabelDTO(long id)
        {
            this.id = id;
        }

        public LabelDTO(long id, string value)
        {
            this.id = id;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as LabelDTO;
            return dTO != null &&
                   id == dTO.id &&
                   value == dTO.value;
        }

        public override int GetHashCode()
        {
            var hashCode = -1304145494;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(value);
            return hashCode;
        }
    }
}
