using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }


        private void Update()
        {
            GetComponent<TMP_Text>().text = string.Format("{0:0}", baseStats.GetLevel());
        }
    }
}