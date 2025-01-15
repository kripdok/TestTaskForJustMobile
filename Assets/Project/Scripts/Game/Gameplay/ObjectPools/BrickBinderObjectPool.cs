
using Project.Scripts.Game.Gameplay.View.Bricks;
using Zenject;

namespace Project.Scripts.Game.Gameplay.ObjectPools
{
    public class BrickBinderObjectPool : MonoMemoryPool<BrickViewModel, BrickBinder>
    {
        protected override void Reinitialize(BrickViewModel p1, BrickBinder item)
        {
            base.Reinitialize(p1, item);
            item.Bind(p1);
        }
    }

}
