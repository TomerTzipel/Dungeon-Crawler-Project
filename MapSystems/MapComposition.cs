
namespace MapSystem
{
    public class MapComposition
    {
        private int[] _sectionsCompositions;
        public int Count { get; private set; } = 0;
        public int Total { get; private set; }
        public int Size 
        { 
            get { return _sectionsCompositions.Length; }
        }

        public MapComposition(int[] compositions)
        {
            _sectionsCompositions = new int[compositions.Length];
            Array.Copy(compositions, _sectionsCompositions, compositions.Length); 

            for(int i = 0; i < _sectionsCompositions.Length; i++)
            {
                Count += _sectionsCompositions[i];
            }

            Total = Count;
        }

        public SectionType PullType(int index)
        {
            if(_sectionsCompositions[index] == 0)
            {
                return SectionType.Error;
            }

            _sectionsCompositions[index]--; 
            Count--;

            return (SectionType)index;
        }

        public bool IsEmpty()
        {
           return Count == 0;
        }


    }
}
