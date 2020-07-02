using System;

namespace PonyForest.Networking.Common.Types
{
    [Serializable]
    public class NetworkVector3
    {
        // non-capital letter for honor of unity's api.
        public float x;
        public float y;
        public float z;

        public NetworkVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }
}