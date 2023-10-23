using TestTaskProject.Runtime.Gameplay;
using TestTaskProject.Runtime.Services;
using Zenject;

namespace TestTaskProject.Runtime.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public MonoBehaviourInputService MonoBehaviourInputService;
        public PlayableCharacter PlayableCharacter;

        public override void InstallBindings()
        {
            Container.Bind<IInputService>().FromInstance(MonoBehaviourInputService).AsSingle();

            Container.BindInstance(PlayableCharacter).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterMover>().AsSingle();

            Container.BindInterfacesAndSelfTo<CharacterMovementTrajectoryPlayer>().AsSingle();
            Container.Bind<MovementTrajectory>().AsSingle();
        }
    }
}