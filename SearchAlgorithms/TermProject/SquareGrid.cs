using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithms.TermProject
{
    public class SquareGrid : IWeightedGraph<Location>
    {
        public static readonly Location[] DIRS = {
            new Location(1, 0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        };

        private int width, height;
        public HashSet<Location> Walls = new HashSet<Location>();
        public HashSet<Location> Forests = new HashSet<Location>();

        public SquareGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool InBounds(Location id)
        {
            return 0 <= id.X && id.X < width && 0 <= id.Y && id.Y < height;
        }

        public bool Passable(Location id)
        {
            return !Walls.Contains(id);
        }

        public double Cost(Location a, Location b)
        {
            return Forests.Contains(b) ? 5 : 1;
        }

        public IEnumerable<Location> Neighbors(Location id)
        {
            return DIRS.Select(dir => new Location(id.X + dir.X, id.Y + dir.Y)).Where(next => InBounds(next) && Passable(next));
        }
    }
}