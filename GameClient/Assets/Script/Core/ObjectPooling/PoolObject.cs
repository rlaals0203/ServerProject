using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public void Pop()
    {
        gameObject.SetActive(true);
    }

    public void Push()
    {

    }
}
