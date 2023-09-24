using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = nameof(ConfigsLevel), menuName = "Configs/" + nameof(ConfigsLevel), order = 0)]
    public class ConfigsLevel : ScriptableObject
    {
        [field: SerializeField] public int levelColumns { get; private set; }
        [field: SerializeField] public int levelLines { get; private set; }
        [field: SerializeField] public List<int> conditionBox  { get; private set; }
        [field: SerializeField] public List<GameObject> boxes { get; private set; }
    }
}


