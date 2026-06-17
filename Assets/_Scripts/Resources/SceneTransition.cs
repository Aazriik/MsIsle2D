using System.Collections;
using UnityEngine;

public abstract class SceneTransition : MonoBehaviour
{
    // Coroutines for Animating into and out of scenes.
    public abstract IEnumerator AnimateTransitionIN();
    public abstract IEnumerator AnimateTransitionOUT();
}
