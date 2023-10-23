using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Zenject;

namespace TestTaskProject.Runtime.Gameplay
{
    public class CharacterMover : IInitializable
    {
        private PlayableCharacter character;
        private TweenerCore<Vector3, Vector3, VectorOptions> moveTween;
        private Vector3 targetPoint;
        private bool movingNow;

        [Inject]
        public CharacterMover(PlayableCharacter character) =>
            this.character = character;

        public void Initialize() =>
            character.OnSpeedChanged += UpdateMoveTween;

        public async UniTask MoveTo(Vector3 point)
        {
            if (movingNow)
                return;

            targetPoint = new Vector3(point.x, point.y, character.transform.position.z);
            PlayMoveTween();
            movingNow = true;

            await UniTask.WaitUntil(() => character.transform.position == targetPoint);
            movingNow = false;
        }

        private void UpdateMoveTween()
        {
            if (moveTween == null || moveTween.IsActive())
                return;

            moveTween?.Kill();
            PlayMoveTween();
        }

        private void PlayMoveTween()
        {
            var duration = Vector3.Distance(character.transform.position, targetPoint) / character.Speed;

            moveTween = character.transform
                .DOMove(targetPoint, duration)
                .SetEase(Ease.Linear);
        }
    }
}