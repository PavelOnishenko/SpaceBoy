using UnityEngine;
using Assets.Scripts.Menu;
using Assets.Scripts.Game.Global;

public class EnemyPlacer : MonoBehaviour
{
    private ExclusiveRendering<CharacterType> exclusiveRendering;

    private void Awake()
    {
        exclusiveRendering = new ExclusiveRendering<CharacterType>(this.gameObject, 
            new[] { CharacterType.Brainman, CharacterType.Lizard, CharacterType.Octopus }, 
            () => IntersceneState.Instance.SelectedEnemy);
        exclusiveRendering.Render();
    }
}