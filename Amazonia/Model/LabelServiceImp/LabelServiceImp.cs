using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public class LabelServiceImp : ILabelService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

        public void CreateLabel(string value, long commentId)
        {
            Label newLabel = new Label();
            newLabel.value = value;

            Comment relatedComment = CommentDao.Find(commentId);
            newLabel.Comments.Add(relatedComment);

            LabelDao.Create(newLabel);
        }

        public void DeleteLabel(long labelId)
        {
            LabelDao.Remove(labelId);
        }

        public List<Label> FindALlLabels()
        {
            return LabelDao.GetAllElements();
        }

        public List<Label> FindLabelsByComment(long commendId)
        {
            Comment relatedComment = CommentDao.Find(commendId);
            List<Label> result = new List<Label>();
            result = LabelDao.FindLabelsOfComment(relatedComment);
            return result;
        }
    }
}
