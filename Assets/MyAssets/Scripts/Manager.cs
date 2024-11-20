using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject[]> _objects = new();

    public static int nodeCount = 0;
}
