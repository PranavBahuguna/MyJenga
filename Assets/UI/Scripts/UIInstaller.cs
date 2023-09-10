using MyJenga.UI.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace MyJenga.UI.Scripts
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiCanvas;
        [SerializeField] private HelpPanelUI _helpPanelUI;
        [SerializeField] private InfoPanelUI _infoPanelUI;
        [SerializeField] private LeftToolbarUI _leftToolbarUI;
        [SerializeField] private QuitPanelUI _quitPanelUI;
        [SerializeField] private RightToolbarUI _rightToolbarUI;

        public override void InstallBindings()
        {
            Container.Bind<IHelpPanelUI>().To<HelpPanelUI>().FromComponentInNewPrefab(_helpPanelUI).UnderTransform(_uiCanvas).AsSingle();
            Container.Bind<IInfoPanelUI>().To<InfoPanelUI>().FromComponentInNewPrefab(_infoPanelUI).UnderTransform(_uiCanvas).AsSingle();
            Container.Bind<ILeftToolbarUI>().To<LeftToolbarUI>().FromComponentInNewPrefab(_leftToolbarUI).UnderTransform(_uiCanvas).AsSingle();
            Container.Bind<IQuitPanelUI>().To<QuitPanelUI>().FromComponentInNewPrefab(_quitPanelUI).UnderTransform(_uiCanvas).AsSingle();
            Container.Bind<IRightToolbarUI>().To<RightToolbarUI>().FromComponentInNewPrefab(_rightToolbarUI).UnderTransform(_uiCanvas).AsSingle();
        }
    }
}