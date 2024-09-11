using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

internal class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxSpeed;
    private void Update()
    {
        if (!GameManager.Instance.StartedLevel)
            return;
        if (GameManager.Instance.EndedRun)
            return;
        transform.position += new Vector3(Time.deltaTime * _parallaxSpeed,0, 0);
    }
}