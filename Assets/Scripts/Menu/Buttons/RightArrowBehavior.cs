using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviors.Menu
{
    internal class RightArrowBehavior : MonoBehaviour
    {
        [SerializeField] private Sprite offSprite;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private GameObject menuStateContainer;

        private MenuState menuState;

        private Image imageComponent;

        private void Start () 
        {
            menuState = menuStateContainer.GetComponent<MenuState>();
            imageComponent = GetComponent<Image>();
        }

        public void Enter() => imageComponent.sprite = onSprite;

        public void Exit() => imageComponent.sprite = offSprite;

        public void Click() => menuState.SelectLevelToRight();
    }
}