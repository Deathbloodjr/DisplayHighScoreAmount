using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace DisplayHighScoreAmount.Patches
{
    internal class DisplayHighScoreAmountPatch
    {
        static int previousHighScoreP1 = 0;
        //static int previousHighScoreP2 = 0;

        [HarmonyPatch(typeof(ResultPlayer))]
        [HarmonyPatch(nameof(ResultPlayer.DisplayMain))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void ResultPlayer_DisplayMain_Postfix(ResultPlayer __instance, EnsoData.Results results, EnsoData.EnsoLevelType course)
        {
            GameObject highScoreTextObject = GameObject.Find("UiTextBestScore");
            var text = highScoreTextObject.GetComponent<TextMeshProUGUI>();
            if (Plugin.Instance.ConfigReplaceText.Value)
            {
                text.text = (results.ensoPlayerResult[0].score - previousHighScoreP1) + "↑";
            }
            else
            {
                text.text += " " + (results.ensoPlayerResult[0].score - previousHighScoreP1) + "↑";
            }
        }

        [HarmonyPatch(typeof(CourseSelect))]
        [HarmonyPatch(nameof(CourseSelect.EnsoConfigSubmit))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void CourseSelect_EnsoConfigSubmit_Postfix(CourseSelect __instance)
        {
            previousHighScoreP1 = TaikoSingletonMonoBehaviour<CommonObjects>.Instance.MyDataManager.EnsoData.ensoSettings.ensoPlayerSettings[0].hiScore;
            //previousHighScoreP2 = TaikoSingletonMonoBehaviour<CommonObjects>.Instance.MyDataManager.EnsoData.ensoSettings.ensoPlayerSettings[1].hiScore;
        }
    }
}
