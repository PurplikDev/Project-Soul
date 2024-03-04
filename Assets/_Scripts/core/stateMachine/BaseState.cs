using System;
using UnityEngine;

namespace roguelike.core.statemachine {
    public abstract class BaseState<T> where T : Enum {

        public T StateKey { get; private set; }

        public BaseState(T key) {
            StateKey = key;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract T GetNextState();
    }
}