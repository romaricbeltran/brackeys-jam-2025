using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds in game action to buttons binding through a list of a custom class ActionKeysBinding.
/// Right now the logic works on HOLDING the buttons.
/// </summary>
public class InputBinder : MonoBehaviour
{
    // [SerializeField] List<ActionKeysBinding> _actionKeysBinding;
    [SerializeField] ActionKeysSO _actionKeysData;

    #region PUBLIC METHODS

    public bool GetMoveLeftInput()
    {
        return GetInputTypeInternal(InGameActionType.MoveLeft);
    }
    public bool GetMoveRightInput()
    {
        return GetInputTypeInternal(InGameActionType.MoveRight);
    }
    public bool GetMoveUpInput()
    {
        return GetInputTypeInternal(InGameActionType.MoveUp);
    }
    public bool GetMoveDownInput()
    {
        return GetInputTypeInternal(InGameActionType.MoveDown);
    }
    public bool GetInteractInput()
    {
        List<KeyCode> targetKeys = new List<KeyCode>();
        TryGetTargetKeys(InGameActionType.Interact, out targetKeys);

        foreach (var key in targetKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetDashInput()
    {
        List<KeyCode> targetKeys = new List<KeyCode>();
        TryGetTargetKeys(InGameActionType.Dash, out targetKeys);

        foreach (var key in targetKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region PRIVATE METHODS

    private bool GetInputTypeInternal(InGameActionType inputType)
    {
        List<KeyCode> targetKeys = new List<KeyCode>();
        TryGetTargetKeys(inputType, out targetKeys);

        foreach (var key in targetKeys)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }

    private bool TryGetTargetKeys(InGameActionType targetInput, out List<KeyCode> keyCodes)
    {
        keyCodes = new List<KeyCode>();

        foreach (var inputTypeKey in _actionKeysData.ActionKeysBindings)
        {
            if (inputTypeKey.targetInputType == targetInput)
            {
                keyCodes = inputTypeKey.targetKeys;
                return true;
            }
        }
        return false;
    }

    #endregion
}

[Flags]
public enum InGameActionType
{
    None = 1 << 0,
    All = 1 << 1,
    MoveLeft = 1 << 2,
    MoveUp = 1 << 3,
    MoveRight = 1 << 4,
    MoveDown = 1 << 5,
    Interact = 1 << 6,
    Dash = 1<<7,
}

[Serializable]
public class ActionKeysBinding
{
    public InGameActionType targetInputType;
    public List<KeyCode> targetKeys;
}