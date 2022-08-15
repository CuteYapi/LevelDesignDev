using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterViewModel : MonoBehaviour
{
    public PlayerCharacterViewModel I { get; private set; }

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

    private void Update()
    {
        PlayerCharacterControl();
    }

    void PlayerCharacterControl()
    {
        playerCharacterMoveSpeed = PlayerCharacterModel.I.characterMoveSpeed;

        #region Character Move
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += playerCharacterMoveSpeed * Vector3.up;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += playerCharacterMoveSpeed * Vector3.down;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += playerCharacterMoveSpeed * Vector3.left;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += playerCharacterMoveSpeed * Vector3.right;
            playerCharacterAnimator.SetBool("isRun", true);
        }

        if (!(Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.W))
            )
        {
            playerCharacterAnimator.SetBool("isRun", false);
        }
        #endregion

        if (Input.GetMouseButton(0))
        {
            playerCharacterAnimator.Play("MayanTribeKnight_Attack");
        }
    }
}
