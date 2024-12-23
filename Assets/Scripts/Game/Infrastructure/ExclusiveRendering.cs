﻿using System.Collections.Generic;
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
            Func<TEnum, GameObject> findObjectInContainerByName = 
                x => host.transform.Cast<Transform>().Single(transform => transform.gameObject.name.Contains(x.ToString())).gameObject;
            group = groupEnumValues.ToDictionary(x => x, findObjectInContainerByName);
        }
        
        public void Render()
        {
            foreach (var go in group.Values) 
                go.SetActive(false);
            group[getSelectedValue()].SetActive(true);
        }
    }
}