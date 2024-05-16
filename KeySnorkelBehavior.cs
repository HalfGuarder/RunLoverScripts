using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySnorkelBehavior : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems _itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.GetTarget);

            InventoryManager.Instance.AddItem(_itemType);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            if(KeySnorkelManager.keySnorkel == 0)
            {
                KeySnorkelManager.keySnorkel = 1;
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
