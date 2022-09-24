using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ViewModel : MonoBehaviour
{
    public static UI_ViewModel I { get; set; }

    [SerializeField] private Sprite[] ButtonSprite;
    [SerializeField] private Image[] MovementButton;
    [SerializeField] private Image[] SkillButton;
    [SerializeField] private Text[] BoardValue;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void Start()
    {
        RenewalBoard("Time");
        RenewalBoard("KillCount");
        RenewalBoard("Score");
    }

    public void ActivateButton(string _buttonTag ,string _buttonID)
    {
        #region MovementButton
        if (_buttonTag == "MovementButton" && _buttonID == "W")
        {
            MovementButton[0].sprite = ButtonSprite[0];
        }

        else if (_buttonTag == "MovementButton" && _buttonID == "A")
        {
            MovementButton[1].sprite = ButtonSprite[0];
        }

        else if (_buttonTag == "MovementButton" && _buttonID == "S")
        {
            MovementButton[2].sprite = ButtonSprite[0];
        }

        else if (_buttonTag == "MovementButton" && _buttonID == "D")
        {
            MovementButton[3].sprite = ButtonSprite[0];
        }
        #endregion

        #region SkillButton
        else if (_buttonTag == "SkillButton" && _buttonID == "skill_fireball01")
        {
            SkillButton[0].sprite = ButtonSprite[0];
        }

        else if (_buttonTag == "SkillButton" && _buttonID == "skill_meteor01")
        {
            SkillButton[1].sprite = ButtonSprite[0];
        }

        else if (_buttonTag == "SkillButton" && _buttonID == "skill_iceshield01")
        {
            SkillButton[2].sprite = ButtonSprite[0];
        }

        else if (_buttonTag == "SkillButton" && _buttonID == "skill_axethrow01")
        {
            SkillButton[3].sprite = ButtonSprite[0];
        }
        #endregion
    }

    public void DeactivateButton(string _buttonTag, string _buttonID)
    {
        #region MovementButton
        if (_buttonTag == "MovementButton" && _buttonID == "W")
        {
            MovementButton[0].sprite = ButtonSprite[1];
        }

        else if (_buttonTag == "MovementButton" && _buttonID == "A")
        {
            MovementButton[1].sprite = ButtonSprite[1];
        }

        else if (_buttonTag == "MovementButton" && _buttonID == "S")
        {
            MovementButton[2].sprite = ButtonSprite[1];
        }

        else if (_buttonTag == "MovementButton" && _buttonID == "D")
        {
            MovementButton[3].sprite = ButtonSprite[1];
        }
        #endregion

        #region SkillButton
        else if (_buttonTag == "SkillButton" && _buttonID == "skill_fireball01")
        {
            SkillButton[0].sprite = ButtonSprite[1];
        }

        else if (_buttonTag == "SkillButton" && _buttonID == "skill_meteor01")
        {
            SkillButton[1].sprite = ButtonSprite[1];
        }

        else if (_buttonTag == "SkillButton" && _buttonID == "skill_iceshield01")
        {
            SkillButton[2].sprite = ButtonSprite[1];
        }

        else if (_buttonTag == "SkillButton" && _buttonID == "skill_axethrow01")
        {
            SkillButton[3].sprite = ButtonSprite[1];
        }
        #endregion
    }

    public void RenewalBoard(string _boardTag)
    {
        if(_boardTag == "Time")
        {
            BoardValue[0].text = CommonValueData.I.CurrentTime.ToString("F1");
        }

        else if(_boardTag == "KillCount")
        {
            BoardValue[1].text = CommonValueData.I.CurrentKillCount.ToString();
        }

        else if(_boardTag == "Score")
        {
            BoardValue[2].text = CommonValueData.I.CurrentScore.ToString();
        }
    }
}
