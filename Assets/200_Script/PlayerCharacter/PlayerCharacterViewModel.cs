using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterViewModel : MonoBehaviour
{
    public static PlayerCharacterViewModel I { get; private set; }

    private float playerCharacterMoveSpeed;

    [SerializeField] private Animator playerCharacterAnimator;

    private void Awake()
    {
        if( I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void FixedUpdate()
    {
        PlayerCharacterControl();
    }

    void PlayerCharacterControl()
    {
        playerCharacterMoveSpeed = PlayerCharacterModel.I.characterMoveSpeed;

        #region Character Move
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.up;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.down;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.left;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.right;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (!(Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.W)
            ))
        {
            playerCharacterAnimator.SetBool("isRun", false);
        }
        #endregion
    }
}
