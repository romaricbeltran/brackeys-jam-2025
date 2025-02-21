using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class holds in game action to buttons binding through a list of a custom class ActionKeysBinding.
/// Right now the logic works on HOLDING the buttons.
/// </summary>
public class InputBinder : MonoBehaviour
{
    public Action OnDashCooldownFinished;

    [SerializeField] ActionKeysSO _actionKeysData;
    [SerializeField] private float _dashCooldown = .5f;

    private bool m_isDashCooldown;
    private Coroutine m_dashCor;

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
        TryGetTargetKeys(InGameActionType.Interact, out List<KeyCode> targetKeys);

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
        if (m_isDashCooldown) return false; // GUARD CASE

        TryGetTargetKeys(InGameActionType.Dash, out List<KeyCode> targetKeys);

        foreach (var key in targetKeys)
        {
            if (Input.GetKeyDown(key))
            {
                m_dashCor = StartCoroutine(DashCooldownCor(_dashCooldown));
                return true;
            }
        }
        return false;
    }

    public bool GetPauseInput()
    {
        TryGetTargetKeys(InGameActionType.Pause, out List<KeyCode> targetKeys);

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
        TryGetTargetKeys(inputType, out List<KeyCode> targetKeys);

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

    private IEnumerator DashCooldownCor(float duration)
    {
        m_isDashCooldown = true;

        yield return new WaitForSeconds(duration);

        OnDashCooldownFinished?.Invoke();
        m_isDashCooldown = false;
        m_dashCor = null;
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
    Dash = 1 << 7,
    Pause = 1 << 8
}

[Serializable]
public class ActionKeysBinding
{
    public InGameActionType targetInputType;
    public List<KeyCode> targetKeys;
}