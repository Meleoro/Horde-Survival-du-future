using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      /*var exp = collision.GetComponent<EXP>();
      if (exp != null)
      {
         exp.Collect();
      }*/
      
      ICollectible collectible = collision.GetComponent<ICollectible>();
      if (collectible != null)
      {
         collectible.Collect();
      }
   }
}
