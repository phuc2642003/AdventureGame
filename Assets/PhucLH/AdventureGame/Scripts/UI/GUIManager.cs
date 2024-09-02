using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class GUIManager : Singleton<GUIManager>
    {
        public Text liveCountingTxt;
        public Text hpCountingTxt;
        public Text coinCountingTxt;
        public Text timeCountingTxt;
        public Text bulletCountingTxt;
        public Text keyCountingTxt;
        public GameObject mobileGamepad;

        public Dialog settingDialog;
        public Dialog pauseDialog;
        public Dialog lvClearedDialog;
        public Dialog lvFailedDialog;

        public override void Awake()
        {
            base.Awake();
        }

        public void UpdateTxt(Text txt, string content)
        {
            if (txt)
            {
                txt.text = content;
            }
        }

        public void UpdateLive(int live)
        {
            UpdateTxt(liveCountingTxt, "x" + live.ToString());
        }

        public void UpdateHp(int hp)
        {
            UpdateTxt(hpCountingTxt, "x" + hp.ToString());
        }

        public void UpdateCoin(int coin)
        {
            UpdateTxt(coinCountingTxt, "x" + coin.ToString());
        }

        public void UpdatePlayTime(string time)
        {
            UpdateTxt(timeCountingTxt, time.ToString());
        }

        public void UpdateBullet(int bullet)
        {
            UpdateTxt(bulletCountingTxt, "x" + bullet.ToString());
        }

        public void UpdateKey(int key)
        {
            UpdateTxt(keyCountingTxt, "x" + key.ToString());
        }

        public void ShowMobileGamepad(bool isShow)
        {
            if (mobileGamepad)
            {
                mobileGamepad.SetActive(isShow);
            }
        }
    }

}
