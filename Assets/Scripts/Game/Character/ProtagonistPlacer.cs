using UnityEngine;
using Assets.Scripts.Menu;
using Assets.Scripts.Game.Global;

public class ProtagonistPlacer : MonoBehaviour
{
    private ExclusiveRendering<CharacterType> exclusiveRendering;

    private void Awake()
    {
        exclusiveRendering = new ExclusiveRendering<CharacterType>(this.gameObject,
            new[] { CharacterType.SpaceGirl, CharacterType.GreenGirl, CharacterType.CosmoKnight },
            () => IntersceneState.Instance.SelectedProtagonist);
        exclusiveRendering.Render();
    }
}