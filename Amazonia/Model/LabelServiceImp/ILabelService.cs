using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public interface ILabelService
    {
        ICommentDao CommentDao { set; }
        ILabelDao LabelDao { set; }

        void CreateLabel(string value, long commentId);
        void DeleteLabel(long labelId);
        List<Label> FindALlLabels();
        List<Label> FindLabelsByComment(long commendId);
    }
}
