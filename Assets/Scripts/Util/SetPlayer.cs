using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    private Animator animator;

    // 각 플레이어에 대한 Animator Controller를 SerializeField로 선언
    [SerializeField] private RuntimeAnimatorController GiwoongController;
    [SerializeField] private RuntimeAnimatorController JiYoonController;
    [SerializeField] private RuntimeAnimatorController JihyoController;
    [SerializeField] private RuntimeAnimatorController SunhoController; // TODO :: Animator Controller 만들기

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        string playerName = PlayerPrefs.GetString("setPlayer");

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("저장된 플레이어 이름이 없습니다.");
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
                Debug.LogError($"{playerName}에 대한 애니메이터 컨트롤러를 찾을 수 없습니다.");
                return;
        }

        if (controller != null)
        {
            animator.runtimeAnimatorController = controller;
            Debug.Log($"{playerName}의 애니메이터 컨트롤러가 설정되었습니다.");
        }
        else
        {
            Debug.LogError($"{playerName}에 대한 애니메이터 컨트롤러를 찾을 수 없습니다.");
        }

    }


}
