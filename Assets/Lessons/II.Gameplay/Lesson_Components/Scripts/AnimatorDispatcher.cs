using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        private Dictionary<string, List<Action>> _actionsDictionary = new();

        public void SubscribeOnEvent(string key, Action action)
        {
            if (!_actionsDictionary.ContainsKey(key))
            {
                _actionsDictionary[key] = new List<Action>();
            }

            _actionsDictionary[key].Add(action);
        }

        public void UnsubscribeOnEvent(string key, Action action)
        {
            if (_actionsDictionary.TryGetValue(key, out var actionsList))
            {
                actionsList.Remove(action);
            }
        }

        //Получаем из анимации
        public void ReceiveEvent(string actionKey)
        {
            // Debug.Log($"Action key = {actionKey}");

            if (_actionsDictionary.TryGetValue(actionKey, out var actionsList))
            {
                foreach (var action in actionsList)
                {
                    action?.Invoke();
                }
            }
        }
    }
}