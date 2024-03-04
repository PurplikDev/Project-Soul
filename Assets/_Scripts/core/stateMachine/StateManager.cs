using System;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.statemachine {
    public abstract class StateManager<T> : MonoBehaviour where T : Enum {
        protected Dictionary<T, BaseState<T>> states = new Dictionary<T, BaseState<T>>();
        protected BaseState<T> currentState;

        protected bool isTransitioningState = false;

        protected virtual void Start() {
            currentState.EnterState();
        }

        protected virtual void Update() {
            T nextStateKey = currentState.GetNextState();
            if(nextStateKey.Equals(currentState.StateKey)) {
                currentState.UpdateState();
            } else if(!isTransitioningState) { 
                TransitionToState(nextStateKey);
            }
        }

        protected void TransitionToState(T stateKey) {
            isTransitioningState = true;
            currentState.ExitState();
            currentState = states[stateKey];
            currentState.EnterState();
            isTransitioningState = false;
        }
    }
}