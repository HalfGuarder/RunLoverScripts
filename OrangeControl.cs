using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.GetOrange);

            Destroy(gameObject);
        }
       
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
