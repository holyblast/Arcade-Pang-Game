
using UnityEngine;

namespace PangGame
{
    public class BubbleView : EnemyView
    {
        private Animator _animator;
        private const string Trigger = "Pop";
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Sprite _firstFrameOfAnimationSprite;

        new void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// pass the onTriggerEvent to the controller
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            _enemyService.OnTriggerEnterEvent(_id, other.tag);
        }

        /// <summary>
        /// If no in a menu, request the movement from the controller
        /// </summary>
        private void Update()
        {
            if (_gameService.stopped) return;

            var destination = _enemyService.GetMovement(_id, Time.deltaTime);

            var vec = transform.localPosition;
            vec.x += destination;
            transform.localPosition = vec;
        }

        /// <summary>
        /// Reset the scale of the shrunk bubble.
        /// </summary>
        public override void SetScale(float bounds)
        {
            transform.localScale = Vector3.one * bounds;
        }

        /// <summary>
        /// After creating via factory, a method to set/get color values
        /// </summary>
        public override void SetColor(Color32 color)
        {
            _spriteRenderer.color = color;
        }

        public override Color32 GetColor()
        {
            return _spriteRenderer.color;
        }

        /// <summary>
        /// Trigger the attached animation on this gameobject.
        /// </summary>
        public override void TriggerPopAnimation()
        {
            SetFreezeZorAll(true);
            _animator.SetTrigger(Trigger);
        }

        /// <summary>
        /// factory does not destroy the gameobject, instead returns it to the memoryPool.
        /// reset all the animation to the first state.
        /// </summary>
        public override void SetFirstAnimationFrame()
        {
            _spriteRenderer.sprite = _firstFrameOfAnimationSprite;
            gameObject.SetActive(false);
        }
    }
}