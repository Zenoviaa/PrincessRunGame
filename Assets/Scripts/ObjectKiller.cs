using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class ObjectKiller : MonoBehaviour
{
    public LayerMask layerToKill;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerToKill)
        {
            Destroy(collision.gameObject);
        }

    }
}
