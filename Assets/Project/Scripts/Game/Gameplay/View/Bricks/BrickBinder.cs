using UnityEngine;
using R3;

namespace Project.Scripts.Game.Gameplay.View.Bricks
{
    public class BrickBinder :MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        private BrickViewModel _viewModel;

        public void Bind(BrickViewModel viewModel)
        {
            _viewModel = viewModel;
            _sprite.color = viewModel.Color;
            viewModel.Position.Subscribe(e => transform.position = e);
        }

        //TODO - Добавить подписку на местоположение Вьюхи. Нужно для перемешения объекта
    }
}
