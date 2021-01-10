using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs
{
    public class LabelMapper
    {
        public static List<LabelDTO> toLabelDTOList(List<Label> labels)
        {
            List<LabelDTO> labelDTOs = new List<LabelDTO>();

            for (int i = 0; i < labels.Count; i++)
            {
                labelDTOs.Add(new LabelDTO(labels[i].id, labels[i].value));
            } 

            return labelDTOs;
        }

        public static List<LabelDTO> toLabelDTOList(ICollection<Label> labels)
        {
            List<LabelDTO> labelDTOs = new List<LabelDTO>();

            foreach (Label label in labels)
            {
                labelDTOs.Add(new LabelDTO(label.id, label.value));
            }

            return labelDTOs;
        }

        public static LabelDTO toLabelDTO(Label label)
        {
            return new LabelDTO(label.id, label.value);
        }
    }
}
