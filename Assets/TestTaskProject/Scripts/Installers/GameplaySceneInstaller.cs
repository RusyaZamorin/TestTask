using TestTaskProject.Gameplay;
using TestTaskProject.Services;
using TestTaskProject.UI;
using Zenject;

namespace TestTaskProject.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public MonoBehaviourInputService MonoBehaviourInputService;
        public PlayableCharacter PlayableCharacter;
        public MovementTrajectoryViewer MovementTrajectoryViewer;

        public override void InstallBindings()
        {
            Container.Bind<IInputService>().FromInstance(MonoBehaviourInputService).AsSingle();

            Container.BindInstance(PlayableCharacter).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterMover>().AsSingle();

            Container.BindInterfacesAndSelfTo<CharacterMovementTrajectoryPlayer>().AsSingle();
            Container.Bind<MovementTrajectory>().AsSingle();
            
            Container.Bind<IInitializable>().FromInstance(MovementTrajectoryViewer).AsSingle();
        }
    }
}