using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs
{
    [Serializable()]
    public class CommentDTO
    {
        public long id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public System.DateTime date { get; set; }
        public long productId { get; set; }
        public long clientId { get; set; }
        public string clientLogin { get; set; }
        public List<LabelDTO> labels { get; set; }
        public string formattedLabels { get; set; }

        public CommentDTO()
        {
        }

        public CommentDTO(long id, string title, string value, DateTime date, long productId, long clientId, string clientLogin, List<LabelDTO> labels)
        {
            this.id = id;
            this.title = title;
            this.value = value;
            this.date = date;
            this.productId = productId;
            this.clientId = clientId;
            this.clientLogin = clientLogin;
            this.labels = labels;
            this.formattedLabels = GenerateFormattedLabels(labels);
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as CommentDTO;
            return dTO != null &&
                   id == dTO.id &&
                   title == dTO.title &&
                   value == dTO.value &&
                   date == dTO.date &&
                   productId == dTO.productId &&
                   clientId == dTO.clientId &&
                   clientLogin == dTO.clientLogin &&
                   EqualityComparer<List<LabelDTO>>.Default.Equals(labels, dTO.labels);
        }

        private string GenerateFormattedLabels(List<LabelDTO> labelDTOs)
        {
            string result = "";

            for (int i = 0; i < labelDTOs.Count; i++)
            {
                if (i == labelDTOs.Count - 1)
                {
                    result += labelDTOs[i].value;
                    break;
                }

                result += labelDTOs[i].value + ", ";
            }
            return result;
        }

        public override int GetHashCode()
        {
            var hashCode = 1257116738;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(value);
            hashCode = hashCode * -1521134295 + date.GetHashCode();
            hashCode = hashCode * -1521134295 + productId.GetHashCode();
            hashCode = hashCode * -1521134295 + clientId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(clientLogin);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<LabelDTO>>.Default.GetHashCode(labels);
            return hashCode;
        }
    }
}
