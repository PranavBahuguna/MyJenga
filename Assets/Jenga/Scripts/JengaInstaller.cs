using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MyJenga.Jenga.Scripts
{
    public class JengaInstaller : MonoInstaller
    {
        [SerializeField] private JengaBlock _blockPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<IEnumerable<GradesData>, JengaTower, JengaTower.Factory>()
                .FromIFactory(x => x.To<JengaTower.CustomFactory>()
                    .AsCached()
                    .WithArguments(_blockPrefab))
                .NonLazy();

            Container.BindInterfacesTo<JengaTowerManager>().AsSingle();
        }
    }
}