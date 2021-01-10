using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao
{
    public class LabelDaoEntityFramework : GenericDaoEntityFramework<Label, long>, ILabelDao
    {

        public List<Label> FindLabelsOfComment(long commentId)
        {
            // TODO
            DbSet<Label> labelList = Context.Set<Label>();

            List<Label> result =
                (from l in labelList
                where l.Comments.Select(c => c.id).Contains(commentId)
                orderby l.id descending
                select l).ToList<Label>();

            return result;

        }

        public List<LabelDTO> FindMostUsedLabels(int count)
        {
            DbSet<Label> labelList = Context.Set<Label>();

            // We only select the id in order to be as efficient as possible
            List<Label> labels =
                (from l in labelList
                 orderby l.Comments.Count descending
                 select l).ToList<Label>();

            // The labelDTOs are created
            List<LabelDTO> result = new List<LabelDTO>();
            for (int index = 0; index < labels.Count && index < count; index++)
            {
                result.Add(LabelMapper.toLabelDTO(labels[index]));
            }

            return result; 
        }

        public int GetNumberOfCommentsForLabel(long labelId)
        {
            Label label = Find(labelId);

            return label.Comments.Count();
        }
    }

}
