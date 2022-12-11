using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombGlowAndSound : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRender;
    [SerializeField] Material redGlow;
    [SerializeField] Material whiteGlow;
    [SerializeField] Material greenGlow;
    [SerializeField] AudioSource beepSound;
    [SerializeField] bool isRed;
    [SerializeField] bool playSound = true;
    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = doTickBomb();
        StartCoroutine(coroutine);
    }


    public IEnumerator doTickBomb()
    {
        if (!isRed)
        {
            changeToGreenMat();
            yield break;
        }
        while (isRed)
        {
            if (playSound)
            {
                beepSound.Play();
            }
            meshRender.material = redGlow;
            yield return new WaitForSeconds(0.6f);
            meshRender.material = whiteGlow;
            yield return new WaitForSeconds(0.21f);
        }
    }

    public void changeToGreenMat()
    {
        meshRender.material = greenGlow;
    }
}


