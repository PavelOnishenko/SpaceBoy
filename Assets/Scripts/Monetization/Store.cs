using UnityEngine;

namespace Assets.Scripts.Monetization
{
    public static class Store
    {
        public static int Money {  get; private set; }

        const int cost = 3;

        public static bool Buy()
        {
            if(Money >= cost) 
            {
                Money -= cost;
                Debug.Log($"Bought ad-free day for [{cost}] coins.");
                return true;
            }
            return false;
        }
    }
}