using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs
{
    public class CommentMapper
    {
        public static CommentDTO CommentToCommentDTO(Comment input)
        {
            return new CommentDTO(input.id, input.title, input.value, input.date, input.productId, input.clientId, input.Client.login, LabelMapper.toLabelDTOList(input.Labels));
        }

        public static List<CommentDTO> CommentListToCommentDTOList(List<Comment> input)
        {
            List<CommentDTO> result = new List<CommentDTO>();

            for (int i = 0; i < input.Count; i++)
            {
                result.Add(CommentToCommentDTO(input[i]));
            }

            return result;
        }
    }
}
