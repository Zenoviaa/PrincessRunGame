using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

internal class ParallaxUp : MonoBehaviour
{
    [SerializeField] private float _parallaxSpeed;
    private void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * _parallaxSpeed, 0);
    }
}