using Core;
using UnityEngine;
using UnityEngine.InputSystem;

public static class UserInputUtility
{
    public static Vector2 GetCurrentPointer()
    {
        return Mouse.current.position.ReadValue();
    }
}