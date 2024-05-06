

namespace GameSystem
{
    public class Camera
    {
        public int Width { init;  get; }
        public int Height { init; get; }

        

        private Point _pivot = new Point();

        public Point Pivot
        {
            get 
            { 
                _pivot.X = PlayerManager.PlayerElement.Position.X - (Width / 2);
                _pivot.Y = PlayerManager.PlayerElement.Position.Y - (Height / 2); 
                return _pivot; 
            }
        }
        public Camera(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
