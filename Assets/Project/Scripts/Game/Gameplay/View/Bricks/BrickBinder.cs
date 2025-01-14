using UnityEngine;
using R3;
using Project.Scripts.Utils;

namespace Project.Scripts.Game.Gameplay.View.Bricks
{
    public class BrickBinder :MonoBehaviour , IRespondOnHold
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Rigidbody2D _body2D;
        [field: SerializeField] public Collider2D Collider { get; private set; }

        private BrickViewModel _viewModel;

        public void Bind(BrickViewModel viewModel)
        {
            _viewModel = viewModel;
            _sprite.color = viewModel.Color;
            transform.localScale = viewModel.Scale;
            viewModel.Position.Subscribe(e => transform.position = e);
        }

        public void StartHold()
        {
            _viewModel.RequestStartHold();
        }
    }
}
