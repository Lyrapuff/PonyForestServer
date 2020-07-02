using System;

namespace PonyForest.Networking.Common.Types
{
    [Serializable]
    public class NetworkQuaternion
    {
        // non-capital letter for honor of unity's api.
        public float x;
        public float y;
        public float z;
        public float w;

        public NetworkQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        
        public override string ToString()
        {
            return $"({x}, {y}, {z}, {w})";
        }
    }
}