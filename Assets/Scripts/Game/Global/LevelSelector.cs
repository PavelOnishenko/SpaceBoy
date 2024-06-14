using UnityEngine;
using Assets.Scripts.Menu;
using Assets.Scripts.Game.Global;

public class LevelSelector : MonoBehaviour
{
    private ExclusiveRendering<Level> exclusiveRendering;

    private void Awake()
    {
        exclusiveRendering = new ExclusiveRendering<Level>(this.gameObject,
            new[] { Level.Hallway, Level.Window }, () => IntersceneState.Instance.SelectedLevel);
        exclusiveRendering.Render();
    }
}