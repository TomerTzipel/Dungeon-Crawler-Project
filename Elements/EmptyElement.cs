

namespace Elements
{
    public class EmptyElement : Element
    {
        private static EmptyElement outerInstance;
        private static EmptyElement innerInstance;

        public bool IsInner { get; private set; }

        private static bool _areInstancesInit = false;
       
        public static EmptyElement InnerInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeEmptyElements();
                }

                return innerInstance;
            }
        }
        public static EmptyElement OuterInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeEmptyElements();
                }

                return outerInstance;
            }
        }
        private EmptyElement(string identifer, ConsoleColor background) : base(identifer) 
        {
            Background = background;
        }

        private static void InitializeEmptyElements()
        {
            if (!_areInstancesInit)
            {
                innerInstance = new EmptyElement(EMPTY_EI, DEFAULT_EBC);
                innerInstance.IsInner = true;
                outerInstance = new EmptyElement(EMPTY_EI, OUTER_EMPTY_EBC);
                outerInstance.IsInner = false;
                _areInstancesInit = true;
            }
        }
    }
}
