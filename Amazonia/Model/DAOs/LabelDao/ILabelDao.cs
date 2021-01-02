using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao
{
    public interface ILabelDao : IGenericDao<Label, long>
    {
        List<Label> FindLabelsOfComment(long commentId);

        List<LabelDTO> FindMostUsedLabels();

        int GetNumberOfCommentsForLabel(long labelId);
    }

}
