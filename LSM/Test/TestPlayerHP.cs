using TMPro;
using UnityEngine;
using YUI.Agents;
using YUI.Agents.players;

namespace YUI.Test.BGD
{
    public class TestPlayerHP : MonoBehaviour
    {
        private float playerHp;
        public TextMeshProUGUI text;
        private Player _player;


        private void Start()
        {
            _player = PlayerManager.Instance.Player;
            playerHp = _player.GetCompo<AgentHealth>().GetCurrentHp();
            text.text = playerHp.ToString();
        }

        private void Update()
        {
            if(playerHp != _player.GetCompo<AgentHealth>().GetCurrentHp())
            {
                playerHp = _player.GetCompo<AgentHealth>().GetCurrentHp();
                text.text = playerHp.ToString();
            }
        }
    }
}
