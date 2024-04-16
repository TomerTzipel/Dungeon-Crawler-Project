

namespace Elements

{
    public class ObstacleElement : Element
    {
        private static ObstacleElement _verticalWallInstance;
        private static ObstacleElement _horizontalWallInstance;

        private static ObstacleElement _treeInstance;

        private static bool _areInstancesInit = false;

        protected ObstacleElement(string identifer) : base(identifer) { }

        public static ObstacleElement VerticalWallInstance
        {
            get 
            {
                if (!_areInstancesInit)
                {
                    InitializeObstacles();
                }
                
                return _verticalWallInstance;
            }
        }
        public static ObstacleElement HorizontalWallInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeObstacles();
                }
                
                return _horizontalWallInstance;
            }
        }
        public static ObstacleElement TreeInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeObstacles();
                }

                return _treeInstance;
            }
        }
        private static void InitializeObstacles()
        {
            if (!_areInstancesInit)
            {
                _verticalWallInstance = new ObstacleElement(VERTICAL_WALL_EI);
                _horizontalWallInstance = new ObstacleElement(HORIZONTAL_WALL_EI);
                _treeInstance = new ObstacleElement(TREE_EI);
                _treeInstance.Foreground = TREE_EFC;

                _areInstancesInit = true;
            }
        }

        //True: If the obstacle is still present.
        //False: If the obstacle is destroyed.
        public virtual bool HitBy(MovingElement element) 
        {
            return true;
        }
        

    }
}
