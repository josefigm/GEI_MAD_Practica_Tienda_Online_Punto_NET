using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs
{
    public class LabelMapper
    {
        public static List<LabelDTO> toLabelDTOList(ICollection<Label> labels)
        {
            List<LabelDTO> labelDTOs = new List<LabelDTO>();

            foreach (Label label in labels)
            {
                labelDTOs.Add(new LabelDTO(label.id, label.value));
            }

            return labelDTOs;
        }
    }
}
