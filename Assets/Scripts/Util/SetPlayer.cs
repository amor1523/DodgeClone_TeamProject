using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    private Animator animator;

    // �� �÷��̾ ���� Animator Controller�� SerializeField�� ����
    [SerializeField] private RuntimeAnimatorController GiwoongController;
    [SerializeField] private RuntimeAnimatorController JiYoonController;
    [SerializeField] private RuntimeAnimatorController JihyoController;
    [SerializeField] private RuntimeAnimatorController SunhoController; // TODO :: Animator Controller �����

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        string playerName = PlayerPrefs.GetString("setPlayer");

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("����� �÷��̾� �̸��� �����ϴ�.");
            return;
        }

        GetPlayerController(playerName);
    }

    private void GetPlayerController(string playerName)
    {
        RuntimeAnimatorController controller = null;

        switch (playerName)
        {
            case "Giwoong":
                controller = GiwoongController;
                break;
            case "JiYoon":
                controller = JiYoonController;
                break;
            case "Jihyo":
                controller = JihyoController;
                break;
            case "Sunho":
                controller = SunhoController;
                break;
            default:
                Debug.LogError($"{playerName}�� ���� �ִϸ����� ��Ʈ�ѷ��� ã�� �� �����ϴ�.");
                return;
        }

        if (controller != null)
        {
            animator.runtimeAnimatorController = controller;
            Debug.Log($"{playerName}�� �ִϸ����� ��Ʈ�ѷ��� �����Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError($"{playerName}�� ���� �ִϸ����� ��Ʈ�ѷ��� ã�� �� �����ϴ�.");
        }

    }


}
