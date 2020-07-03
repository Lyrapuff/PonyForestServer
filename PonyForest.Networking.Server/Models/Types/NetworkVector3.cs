using System;

namespace PonyForestServer.Core.Models.Types
{
    [Serializable]
    public class NetworkVector3
    {
        // non-capital letters for honor of unity's api.
        public float x;
        public float y;
        public float z;

        public NetworkVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}