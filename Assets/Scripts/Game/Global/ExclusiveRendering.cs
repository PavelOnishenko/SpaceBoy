using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Assets.Scripts.Game.Global
{
    internal class ExclusiveRendering<TEnum> where TEnum : Enum
    {
        private readonly Dictionary<TEnum, GameObject> group;
        private readonly Func<TEnum> getSelectedValue;

        public ExclusiveRendering(GameObject host, IEnumerable<TEnum> groupEnumValues, Func<TEnum> getSelectedValue)
        {
            this.getSelectedValue = getSelectedValue;
            group = groupEnumValues.ToDictionary(x => x, enumValue => host.transform.Cast<Transform>()
                .Single(transform => transform.gameObject.name.Contains(enumValue.ToString())).gameObject);
        }

        public void Render()
        {
            foreach (var go in group.Values) go.SetActive(false);
            var selectedValue = getSelectedValue();
            var selectedGo = group[selectedValue];
            selectedGo.SetActive(true);
        }
    }
}