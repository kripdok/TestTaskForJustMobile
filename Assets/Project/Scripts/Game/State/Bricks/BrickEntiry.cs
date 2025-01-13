using Project.Scripts.Game.State.Entitys;
using System;
using UnityEngine;

namespace Project.Scripts.Game.State.Bricks
{
    [Serializable]
    public class BrickEntiry: Entity
    {
        public string TypeID;
        public Vector3 Position;
        public int Level;
        public Color Color;
    }
}
