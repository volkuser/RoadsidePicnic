using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ForDialogWindow : MonoBehaviour
    {
        public static IEnumerator OneUseWithOne(GameObject dialogWindow, 
            string text, TMP_Text gui, 
            GameObject currentSpeakerFace = null, GameObject desiredSpeakerFace = null,
            float delay = 0.008f /*0.008f*/)
        {
            Sprite bufferSpeakerFace = null;
            if (desiredSpeakerFace != null)
            {
                bufferSpeakerFace = currentSpeakerFace!.GetComponent<Image>().sprite;
                currentSpeakerFace.GetComponent<Image>().sprite =
                    desiredSpeakerFace.GetComponent<SpriteRenderer>().sprite;
            }
        
            dialogWindow.SetActive(true);
        
            gui.text = string.Empty;
            foreach (var symbol in text)
            {
                gui.text += symbol;
                yield return new WaitForSeconds(delay);
            }

            while (true)
            {
                if (!Input.GetKeyDown(KeyCode.N)) yield return new WaitForSeconds(0.01f);
                else
                {
                    dialogWindow.SetActive(false);
                    if (desiredSpeakerFace != null) 
                        currentSpeakerFace!.GetComponent<Image>().sprite = bufferSpeakerFace;
                    break;
                }
            }
        }
    }
}
