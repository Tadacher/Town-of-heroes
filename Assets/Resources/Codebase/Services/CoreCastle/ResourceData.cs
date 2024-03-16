using System;

namespace Progress
{
    [Serializable]
    public class ResourceData
    {
        public int WoodPieces;
        public int StonePieces;
        public int FoodPieces;

        public ResourceData(int woodPieces, int stonePieces, int foodPieces)
        {
            WoodPieces = woodPieces;
            StonePieces = stonePieces;
            FoodPieces = foodPieces;
        }
    }
}