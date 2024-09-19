using UnityEngine;
using Assets.Scripts.Menu;
using Assets.Scripts.Game.Global;
using System;
using System.Linq;

public class LevelSelector : MonoBehaviour
{
    private ExclusiveRendering<Level> exclusiveRendering;

    private void Awake()
    {
        exclusiveRendering = new ExclusiveRendering<Level>(this.gameObject, 
            Enum.GetValues(typeof(Level)).Cast<Level>(), () => IntersceneState.Instance.SelectedLevel);
        exclusiveRendering.Render();
    }
}