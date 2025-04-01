using System;
using UnityEngine;

public class CheckBreak : MonoBehaviour
{
    public OutComeBall OutComeBall;

    private void OnJointBreak(float breakForce)
    {
        OutComeBall.OnJointBreak(breakForce);
    }
}
