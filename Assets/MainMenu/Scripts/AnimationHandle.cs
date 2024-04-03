using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    [Header("Anim")]
        public GameObject clickToStart;
        private Animator animController;
        
    public void CloudsAnimation()
    {
        animController = GetComponent<Animator>();
        animController.SetTrigger("Start");
    }

    public void AnimationEnded()
    {
        clickToStart.SetActive(false);
    }
}
