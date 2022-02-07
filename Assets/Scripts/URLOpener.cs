using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpener : MonoBehaviour
{
    public string Url;
    // Open URL
    public void Open()
    {
        Application.OpenURL(Url);
    }
}
