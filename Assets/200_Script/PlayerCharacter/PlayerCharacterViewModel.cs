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
    }

    void PlayerCharacterControl()
    {
        playerCharacterMoveSpeed = PlayerCharacterModel.I.characterMoveSpeed;

        #region Character Move
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.up;
            playerCharacterAnimator.SetBool("isRun", true);

            playerDirectionVector[1] = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.down;
            playerCharacterAnimator.SetBool("isRun", true);

            playerDirectionVector[1] = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.left;
            playerCharacterAnimator.SetBool("isRun", true);

            playerDirectionVector[0] = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += playerCharacterMoveSpeed * Vector3.right;
            playerCharacterAnimator.SetBool("isRun", true);

            playerDirectionVector[0] = 1;
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
}
