using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.TermProject
{
    public interface IWeightedGraph<L>
    {
        double Cost(Location a, Location b);
        IEnumerable<Location> Neighbors(Location id);
    }
}
