using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfCharacter: MonoBehaviour
{
    public TargetType targetType;
    public enum TargetType
    {
        Player, Enemy
    }
}
