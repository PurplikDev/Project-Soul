using System;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.utils {
    [Serializable]
    public class NonNullList<T> {

        [SerializeField] List<T> _list;
        private T _defaultValue;

        public NonNullList(int size, T defaultValue) { 
            _defaultValue = defaultValue;
            _list = new List<T>(size);

            for (int i = 0; i < size; i++) { 
                _list.Insert(i, _defaultValue);
            }
        }

        public void RemoveAt(int index) {
            _list.Insert(index, _defaultValue);
        }
    }
}