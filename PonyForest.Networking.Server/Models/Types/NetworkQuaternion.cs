using System;

namespace PonyForestServer.Core.Models.Types
{
    [Serializable]
    public class NetworkQuaternion
    {
        // non-capital letters for honor of unity's api.
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
    }
}