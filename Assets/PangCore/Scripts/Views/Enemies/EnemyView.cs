using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// An abstract class mainly resposible inherit, the types to inherit are those that seek to harm the player.
    /// it is controlled by the controller that gave it an id after creating via a factory.
    /// </summary>
    public abstract class EnemyView : MonoBehaviour, IEnemyView
    {
        /// <summary>
        /// an id to locate which view the controller is handling
        /// </summary>
        public string _id;
        protected Rigidbody2D _rigidbody2D;
        
        // store the previous amount of velocity assigned to the rigidbody while pausing the game.
        private Vector2 _previousVelocity = Vector2.zero;

        // DI injections
        [Inject] protected IEnemyService _enemyService;
        [Inject] protected IGameService _gameService;


        protected void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public abstract void SetScale(float bounds);
        public abstract void SetColor(Color32 color);
        public abstract Color32 GetColor();

        // In case of no animation in the inherited script use the parent automatic action
        public virtual void TriggerPopAnimation()
        {
            _enemyService.SplitBubble(_id);
        }

        /// <summary>
        /// the Factory makes use of memoryPooling thus if the id exists we request it rather than creating a new one,
        /// after which we return to the controller.
        /// </summary>
        public string GetId()
        {
            if (string.IsNullOrEmpty(_id))
                _id = Guid.NewGuid().ToString();

            return _id;
        }

        /// <summary>
        /// the Controller needs to know the coordinates for further action. (like where to use the factory to create
        /// more enemies
        /// </summary>
        public Vector2 GetPosition()
        {
            return transform.localPosition;
        }

        /// <summary>
        /// After creating via factory, a method to set it up.
        /// </summary>
        public void SetPosition(Vector2 position)
        {
            transform.localPosition = position;
        }

        /// <summary>
        /// After creating via factory, a method to set the velocity.
        /// </summary>
        public void SetVelocity(float height)
        {
            _rigidbody2D.velocity = new Vector2(0, height);
        }

        /// <summary>
        /// request the enemy to split via the controller.
        /// </summary>
        public void SplitBubble()
        {
            _enemyService.SplitBubble(_id);
        }

        public abstract void SetFirstAnimationFrame();

        /// <summary>
        /// freeze the velocity during menu pause.
        /// </summary>
        public void StopState(bool state)
        {
            if (state)
            {
                _previousVelocity = _rigidbody2D.velocity;
            }
            else
            {
                _rigidbody2D.velocity = _previousVelocity;
            }
            SetFreezeZorAll(state);
        }

        /// <summary>
        /// freeze all constraints during menu pause.
        /// </summary>
        public void SetFreezeZorAll(bool state)
        {
            if (state)
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
