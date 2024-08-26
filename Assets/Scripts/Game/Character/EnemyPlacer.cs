using UnityEngine;
using Assets.Scripts.Menu;
using Assets.Scripts.Game.Global;

public class EnemyPlacer : MonoBehaviour
{
    private ExclusiveRendering<CharacterType> exclusiveRendering;

    private void Awake()
    {
        var enemies = new[] { CharacterType.Brainman, CharacterType.Lizard, CharacterType.Octopus, CharacterType.AstronautGirl };
        exclusiveRendering = new ExclusiveRendering<CharacterType>(this.gameObject, enemies, () => IntersceneState.Instance.SelectedEnemy);
        exclusiveRendering.Render();
    }
}