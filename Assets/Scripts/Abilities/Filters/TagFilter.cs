using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    [CreateAssetMenu(fileName = "Tag Filter", menuName = "Abilities/Filters/Tag", order = 0)]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToFilter = "";

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var gameobject in objectsToFilter)
            {
                if (gameobject.CompareTag(tagToFilter) && !gameobject.GetComponent<Health>().IsDead())
                {
                    yield return gameobject;
                }
            }
        }
    }
}