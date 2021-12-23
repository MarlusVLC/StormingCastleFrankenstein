using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class PlayerData
    {
        public PlayerStartingPosition playerStartingPosition;
        public PlayerStartingRotation playerStartingRotation;
        public bool[] unlockedWeapons;

        public PlayerData(Vector3 location, Quaternion rotation)
        {
            playerStartingPosition = new PlayerStartingPosition(location);
            playerStartingRotation = new PlayerStartingRotation(rotation);
            // unlockedWeapons = new bool[4];
            // unlockedWeapons[0] = true;
        }
        
        [System.Serializable]
        public class PlayerStartingPosition
        {
            public float x;
            public float y;
            public float z;

            public PlayerStartingPosition(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public PlayerStartingPosition(Vector3 position) : this(position.x, position.y, position.z)
            {
            }

            public Vector3 ToVector3 => new Vector3(x, y, z);
        }
        
        [System.Serializable]
        public class PlayerStartingRotation
        {
            public float x;
            public float y;
            public float z;
            public float w;
            
            public PlayerStartingRotation(float x, float y, float z, float w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            public PlayerStartingRotation(Quaternion rotation) : this(rotation.x, rotation.y, rotation.z, rotation.w)
            {
            }

            public Quaternion ToQuaternion => new Quaternion(x, y, z, w);
            
        }
    }
}