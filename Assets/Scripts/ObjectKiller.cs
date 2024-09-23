using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class ObjectKiller : MonoBehaviour
{
    public LayerMask layerToKill;
    public float killRadius = 16;

    private void Update()
    {
        foreach(var g in Physics2D.OverlapCircleAll(transform.position, killRadius, layerToKill))
        {
            Destroy(g.gameObject);
        }
    }
}
