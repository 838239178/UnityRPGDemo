using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGTALK.Snippets
{
    [AddComponentMenu("Seize Studios/RPGTalk/Snippets/Save Instance")]
    [ExecuteInEditMode]
    public class RPGTalkSaveInstance : MonoBehaviour
    {

        public bool saveBetweenPlays;
        [Header("Check the checkbox below to erase all saved data")]
        public bool erase;

        RPGTalk rpgTalk;

        List<string> savedInThisPlay = new List<string>();

        // Start is called before the first frame update
        void Start()
        {
            rpgTalk = GetComponent<RPGTalk>();
            rpgTalk.OnMadeChoice += SaveData;
        }

        private void Update()
        {
            if (erase)
            {
                erase = false;
                PlayerPrefs.DeleteAll();
                if (saveBetweenPlays)
                {
                    PlayerPrefs.Save();
                }

            }
        }

        private void OnDestroy()
        {
            rpgTalk.OnMadeChoice -= SaveData;
        }

        void OnApplicationQuit()
        {
            if (!saveBetweenPlays)
            {
                foreach(string todestroy in savedInThisPlay)
                {
                    PlayerPrefs.DeleteKey(todestroy);
                }
                PlayerPrefs.Save();
            }
        }
        /// <summary>
        /// 保存choiceID的value为answerID
        /// </summary>
        /// <param name="choiceID">保存的数据名称</param>
        /// <param name="answerID">判断标志</param>
        public void SaveData(string choiceID, int answerID)
        {
            PlayerPrefs.SetInt(choiceID, answerID);
            savedInThisPlay.Add(choiceID);
            if (saveBetweenPlays)
            {
                PlayerPrefs.Save();
            }
        }
        /// <summary>
        /// 当saveData对应的值等于modifier时返回true;
        /// </summary>
        /// <param name="savedData">保存的数据名称，可以是任意字符串，ex:问题的ID</param>
        /// <param name="modifier">填入判断标志，整数</param>
        /// <returns></returns>
        public bool GetSavedData(string savedData, int modifier)
        {
            if (PlayerPrefs.HasKey(savedData) && PlayerPrefs.GetInt(savedData) == modifier)
            {
                return true;
            }
            else if(!PlayerPrefs.HasKey(savedData) && modifier == -1)
            {
                return true;
            }

            return false;
        }
    }
}