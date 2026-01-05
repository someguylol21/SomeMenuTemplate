using NewTemplate.Mods;
using UnityEngine;
using NewTemplate.Menu;
using static NewTemplate.Menu.Main;
using static NewTemplate.Settings;
using Il2Cpp;

namespace NewTemplate.Classes
{
    public class EspObject : MonoBehaviour
    {
        public FusionPlayer attachedPlayer;

        void Update()
        {
            if (attachedPlayer != null)
            {
                gameObject.transform.position = attachedPlayer.HeadTarget.transform.position + attachedPlayer.HeadTarget.transform.forward * -0.075f + attachedPlayer.HeadTarget.transform.up * -0.3f;
            }
        }
	}
}
