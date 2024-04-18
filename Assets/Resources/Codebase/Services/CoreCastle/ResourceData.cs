using System;

namespace Progress
{
    [Serializable]
    public class ResourceData
    {
        public int WoodPieces;
        public int StonePieces;
        public int FoodPieces;
        public int Scrolls;

        public ResourceData(int woodPieces, int stonePieces, int foodPieces, int scrolls)
        {
            WoodPieces = woodPieces;
            StonePieces = stonePieces;
            FoodPieces = foodPieces;
            Scrolls = scrolls;
        }
    }
}