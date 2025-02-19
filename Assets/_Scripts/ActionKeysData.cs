using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionKeysData : ScriptableObject
{
     [SerializeField] List<ActionKeysBinding> _actionKeysBinding;

     public List<ActionKeysBinding> ActionKeysBindings => _actionKeysBinding;
}
