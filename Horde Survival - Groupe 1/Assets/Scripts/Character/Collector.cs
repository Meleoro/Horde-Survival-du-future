using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      EXP.ICollectible collectible = collision.GetComponent<EXP.ICollectible>();
      if (collectible != null)
      {
         collectible.Collect();
      }
   }
}
