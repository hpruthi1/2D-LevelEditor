using UnityEngine;
using System;

public class EditorObject : MonoBehaviour
{
    public enum ObjectType { StartPos, EndPos, Platform, Coin, Player };

    [Serializable]
    public struct Data
    {
        public Vector2 pos; // the object's position.
        public ObjectType objectType; // the type of object.
    }

    public Data data;
}
