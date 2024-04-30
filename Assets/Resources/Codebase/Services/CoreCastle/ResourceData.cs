using System;

namespace Progress
{
    [Serializable]
    public class ResourceData
    {
        public int WoodPieces;
        public int StonePieces;
        public int FoodPieces;
        public int Waves;

        [NonSerialized]
        public int Scrolls;

        
        public ResourceData(int woodPieces, int stonePieces, int foodPieces, int scrolls, int waves)
        {
            WoodPieces = woodPieces;
            StonePieces = stonePieces;
            FoodPieces = foodPieces;
            Scrolls = scrolls;
            Waves = waves;
        }
        public static bool operator >(ResourceData resourceData1, ResourceData resourceData2)
        {
            
            return resourceData1.Waves > resourceData2.Waves &&
                   resourceData1.Scrolls > resourceData2.Scrolls &&
                   resourceData1.WoodPieces > resourceData2.WoodPieces &&
                   resourceData1.StonePieces > resourceData2.StonePieces &&
                   resourceData1.FoodPieces > resourceData2.FoodPieces;
        }
        public static bool operator >=(ResourceData resourceData1, ResourceData resourceData2)
        {

            return resourceData1.Waves >= resourceData2.Waves &&
                   resourceData1.Scrolls >= resourceData2.Scrolls &&
                   resourceData1.WoodPieces >= resourceData2.WoodPieces &&
                   resourceData1.StonePieces >= resourceData2.StonePieces &&
                   resourceData1.FoodPieces >= resourceData2.FoodPieces;
        }
        public static bool operator <(ResourceData resourceData1, ResourceData resourceData2)
        {
            return resourceData1.Waves < resourceData2.Waves &&
                   resourceData1.Scrolls < resourceData2.Scrolls &&
                   resourceData1.WoodPieces < resourceData2.WoodPieces &&
                   resourceData1.StonePieces < resourceData2.StonePieces &&
                   resourceData1.FoodPieces < resourceData2.FoodPieces;
        }
        public static bool operator <=(ResourceData resourceData1, ResourceData resourceData2)
        {
            return resourceData1.Waves <= resourceData2.Waves &&
                   resourceData1.Scrolls <= resourceData2.Scrolls &&
                   resourceData1.WoodPieces <= resourceData2.WoodPieces &&
                   resourceData1.StonePieces <= resourceData2.StonePieces &&
                   resourceData1.FoodPieces <= resourceData2.FoodPieces;
        }
    }
}