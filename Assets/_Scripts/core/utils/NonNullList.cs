using System;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.utils {
    [Serializable]
    public class NonNullList<T> {

        public List<T> Container;
        private T _defaultValue;

        public NonNullList(int size, T defaultValue) { 
            _defaultValue = defaultValue;
            Container = new List<T>(size);

            for (int i = 0; i < size; i++) {
                Container.Insert(i, _defaultValue);
            }
        }

        public void RemoveAt(int index) {
            Container.Insert(index, _defaultValue);
        }
    }
}