using UnityEngine;

public class UI_FadeScreen : MonoBehaviour
{
    public Animator anim;

    public void FadeOut()
    {
        anim.SetTrigger("fadeOut"); ;
    }
    public void FadeIn()
    {
        anim.Play("fadeIn"); ;
    }


}
