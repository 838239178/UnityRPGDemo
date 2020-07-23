using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public GameObject MenuItem;

    private void OnDestroy()
    {
        Destroy(MenuItem);
    }
}
