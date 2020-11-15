using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public interface ILabelService
    {
        void CreateLabel(string value, long commentId);
        void DeleteLabel(long labelId);
        List<Label> FindALlLabels();
        List<Label> FindLabelsByComment(long commendId);
    }
}
