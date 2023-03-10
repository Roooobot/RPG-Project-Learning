using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana;

        private void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>();
        }


        private void Update()
        {
            GetComponent<TMP_Text>().text = string.Format("{0:0}/{1:0}", mana.GetMana(), mana.GetMaxMana());
        }
    }
}