using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviourSingletonTemplate<MenuSceneManager>
{
    private Keyboard _keyboard = null;

    public List<Button> BtnList;

    private int _index;
    private int _btnMaxCnt;

    private bool _isMouse = false;

    private void Start()
    {
        _keyboard = Keyboard.current;

        _index = 0;
        _btnMaxCnt = BtnList.Count;

        BtnList[0].onClick.AddListener(() => GameStartBtn());
        BtnList[1].onClick.AddListener(() => TutorialBtn());
        BtnList[2].onClick.AddListener(() => SelectCharacterBtn());
        BtnList[3].onClick.AddListener(() => SettingBtn());
        BtnList[4].onClick.AddListener(() => ExitBtn());
    }

    private void Update()
    {
        if (_keyboard.upArrowKey.wasPressedThisFrame)
        {
            ChangeBtn(Color.white);

            _index -= 1;
            if (_index < 0)
                _index = _btnMaxCnt - 1;

            ChangeBtn(Color.yellow);
        }
        else if (_keyboard.downArrowKey.wasPressedThisFrame)
        {
            ChangeBtn(Color.white);

            _index += 1;
            if (_index >= _btnMaxCnt)
                _index = 0;

            ChangeBtn(Color.yellow);
        }
        else if (_keyboard.zKey.wasPressedThisFrame)
        {
            BtnList[_index].onClick.Invoke();
        }
    }

    public void ChangeBtn(Color color)
    {
        BtnList[_index].GetComponent<Image>().color = color;
    }

    public void InBtn(int index)
    {
        _index = index;
        ChangeBtn(Color.yellow);
    }

    public void OutBtn()
    {
        ChangeBtn(Color.white);
    }

    public void GameStartBtn()
    {
        Debug.Log("GameStart");
        SceneManager.LoadScene("CombatScene");
    }

    public void TutorialBtn()
    {
        Debug.Log("Tutorial");
        SceneManager.LoadScene("Practice");
    }

    public void SelectCharacterBtn()
    {
        Debug.Log("SelectCharacter");
    }

    public void SettingBtn()
    {
        Debug.Log("Setting");
    }

    public void ExitBtn()
    {
        Debug.Log("Exit");
    }

}
