using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }


        private void Update()
        {
            GetComponent<TMP_Text>().text = string.Format("{0:0}", experience.GetPoints());
        }
    }
}