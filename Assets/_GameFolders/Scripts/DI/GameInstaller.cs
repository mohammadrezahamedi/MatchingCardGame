using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace MatchingCards
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SaveManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        }

    }
}