using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public class LabelServiceImp : ILabelService
    {
        [Inject]
        public ICommentDao commentDao;
        [Inject]
        public ILabelDao labelDao;

        public void CreateLabel(string value, long commentId)
        {
            Label newLabel = new Label();
            newLabel.value = value;

            Comment relatedComment = commentDao.Find(commentId);
            newLabel.Comments.Add(relatedComment);

            labelDao.Create(newLabel);
        }

        public void DeleteLabel(long labelId)
        {
            labelDao.Remove(labelId);
        }

        public List<Label> FindALlLabels()
        {
            return labelDao.GetAllElements();
        }

        public List<Label> FindLabelsByComment(long commendId)
        {
            Comment relatedComment = commentDao.Find(commendId);
            List<Label> result = new List<Label>();
            result = labelDao.FindLabelsOfComment(relatedComment);
            return result;
        }
    }
}
