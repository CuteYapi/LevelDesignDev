using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterViewModel : MonoBehaviour
{
    public static PlayerCharacterViewModel I { get; private set; }

    private float playerCharacterMoveSpeed;

    [SerializeField] private Animator playerCharacterAnimator;
    private int[] playerDirectionVector = { 0, 1 };
    public Vector3 playerDirection;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void FixedUpdate()
    {
        PlayerCharacterControl();
        PlayerSkillControl();
    }

    void PlayerCharacterControl()
    {
        playerCharacterMoveSpeed = PlayerCharacterModel.I.characterMoveSpeed;

        #region Character Move
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.up;
            playerCharacterAnimator.SetBool("isRun", true);
            UI_ViewModel.I.ActivateButton("MovementButton", "W");

            playerDirectionVector[1] = 1;
        }

        else
        {
            UI_ViewModel.I.DeactivateButton("MovementButton", "W");
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.down;
            playerCharacterAnimator.SetBool("isRun", true);
            UI_ViewModel.I.ActivateButton("MovementButton", "S");

            playerDirectionVector[1] = -1;
        }

        else
        {
            UI_ViewModel.I.DeactivateButton("MovementButton", "S");
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.left;
            playerCharacterAnimator.SetBool("isRun", true);
            UI_ViewModel.I.ActivateButton("MovementButton", "A");

            playerDirectionVector[0] = -1;
        }

        else
        {
            UI_ViewModel.I.DeactivateButton("MovementButton", "A");
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.right;
            playerCharacterAnimator.SetBool("isRun", true);
            UI_ViewModel.I.ActivateButton("MovementButton", "D");

            playerDirectionVector[0] = 1;
        }
        else
        {
            UI_ViewModel.I.DeactivateButton("MovementButton", "D");
        }

        if (!(Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D)))
        {
            playerCharacterAnimator.SetBool("isRun", false);
        }

        else if (!(Input.GetKey(KeyCode.W)
        || Input.GetKey(KeyCode.S)))
        {
            playerDirectionVector[1] = 0;
        }

        else if (!(Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D)))
        {
            playerDirectionVector[0] = 0;
        }

        playerDirection = new Vector3(playerDirectionVector[0], playerDirectionVector[1]);
        #endregion
    }

    void PlayerSkillControl()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SkillViewModel.I.SetSkill("skill_fireball01");
            UI_ViewModel.I.ActivateButton("SkillButton", "skill_fireball01");

            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_meteor01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_iceshield01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_axethrow01");
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SkillViewModel.I.SetSkill("skill_meteor01");
            UI_ViewModel.I.ActivateButton("SkillButton", "skill_meteor01");

            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_fireball01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_iceshield01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_axethrow01");
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            SkillViewModel.I.SetSkill("skill_iceshield01");
            UI_ViewModel.I.ActivateButton("SkillButton", "skill_iceshield01");

            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_fireball01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_meteor01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_axethrow01");
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            SkillViewModel.I.SetSkill("skill_axethrow01");
            UI_ViewModel.I.ActivateButton("SkillButton", "skill_axethrow01");

            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_fireball01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_meteor01");
            UI_ViewModel.I.DeactivateButton("SkillButton", "skill_iceshield01");
        }
    }
}
