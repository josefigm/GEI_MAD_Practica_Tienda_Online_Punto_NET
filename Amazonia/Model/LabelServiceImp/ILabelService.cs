using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public interface ILabelService
    {
        void createLabel(string value, long commentId);
        void deleteLabel(long labelId);
        List<Label> findLabelsByComment(long commendId);
    }
}
