
namespace Elements
{
    public class VaseElement : DestroyableElement
    {
        public VaseElement() :base(VASE_EI) 
        {
            Foreground = VASE_EFC;
            _hp = RandomRange(10, 20);
        }

        protected override void Destroyed()
        {
            //reward drop  
        }
    }
}
