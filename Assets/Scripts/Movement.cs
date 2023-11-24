using Fighters.Match;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 _tilePosition;

    void Start()
    {
        StartCoroutine(ListenForMovement());
    }

    private IEnumerator ListenForMovement()
    {
        while (true)
        {
            if (!MatchManager.MatchStarted) yield return null;

            if (Input.GetKey(KeyCode.W))
            {

            }
        }
    }
}
