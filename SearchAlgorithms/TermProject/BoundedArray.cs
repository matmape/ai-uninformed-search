namespace SearchAlgorithms.TermProject
{
    public class BoundedArray<T>
    {
        private T[] values;

        public BoundedArray(int lower, int upper)
        {
            Lower = lower;
            Upper = upper;
            values = new T[Count];
        }

        public BoundedArray(int lower, int upper, T initial) : this(lower, upper)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = initial;
            }
        }

        public int Lower
        {
            get;
            private set;
        }

        public int Upper
        {
            get;
            private set;
        }

        private int Count
        {
            get { return Upper - Lower + 1; }
        }

        public T this[int index]
        {
            get { return values[index - Lower]; }
            set { values[index - Lower] = value; }
        }
    }
}