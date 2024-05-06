
namespace MapElements
{
    public abstract class Element
    {
        public string Identifier { get; protected set; }
        public ConsoleColor _background = DEFAULT_EBC;
        public ConsoleColor Foreground { get; protected set; } = DEFAULT_EFC;

        public virtual ConsoleColor Background
        {
            get
            {
                return _background;
            }
            protected set
            {
                _background = value;
            }
        }

        public virtual void TurnBloody()
        {
            Background = BLOOD_PUDDLE_EBC;
        }

        public Element(string identifier) 
        {
            Identifier = identifier;
        }



        public override string ToString() 
        {
            return Identifier;
        }
    }
}
