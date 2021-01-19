namespace Brickwork
{
    public class Brick
    {

        public int Size;

        public bool IsRightSideValid;

        public bool AreRightAndBottomSideValid;
        
        public bool IsBaseOfBrickWork;

        public int SecondLayerRow;
        
        public int SecondLayerCol;

        public bool IsLayerValid;

        public Brick(int areaOfSize)
        {
            Size = areaOfSize;
            IsRightSideValid = false;
            AreRightAndBottomSideValid = false;
            IsLayerValid = false;
            IsBaseOfBrickWork = false;
        }
    }
}
