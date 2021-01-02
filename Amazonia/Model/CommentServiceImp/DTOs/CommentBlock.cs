using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs
{
    public class CommentBlock
    {
        public List<CommentDTO> Comments { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentBlock(List<CommentDTO> comments, bool existMoreComments)
        {
            Comments = comments;
            ExistMoreComments = existMoreComments;
        }
    }
}
