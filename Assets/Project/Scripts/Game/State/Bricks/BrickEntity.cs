using Project.Scripts.Game.State.Entitys;
using System;
using UnityEngine;

namespace Project.Scripts.Game.State.Bricks
{
    [Serializable]
    public class BrickEntity: Entity
    {
        public string TypeID;
        public Color Color;
        public Vector3 Scale;
        public Vector3 Position;
    }
}
