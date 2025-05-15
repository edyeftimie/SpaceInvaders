namespace SpaceInvaders
{
    public class MapBoundaryValidator : IValidator<GameObject>
    {
        private static MapBoundaryValidator _instance;
        private static readonly object _lock = new object();

        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;
        public string ErrorMessage { get; private set; } = "";

        // Private constructor to prevent external instantiation
        private MapBoundaryValidator() { }

        // Public static method to get the singleton instance
        public static MapBoundaryValidator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) // Thread safety
                    {
                        if (_instance == null)
                        {
                            _instance = new MapBoundaryValidator();
                        }
                    }
                }
                return _instance;
            }
        }

        // Public initialization method
        public void Initialize(int minX, int maxX, int minY, int maxY)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }

        public bool IsValid(GameObject obj)
        {
            if (obj.x < _minX)
            {
                ErrorMessage = "Object is outside the left boundary.";
                return false;
            }
            if (obj.x + obj.width > _maxX)
            {
                ErrorMessage = "Object is outside the right boundary.";
                return false;
            }
            if (obj.y < _minY)
            {
                ErrorMessage = "Object is outside the top boundary.";
                return false;
            }
            if (obj.y + obj.height > _maxY)
            {
                ErrorMessage = "Object is outside the bottom boundary.";
                return false;
            }
            return true;
        }
    }
}
