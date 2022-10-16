using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

// ทำใหม่ให้ใช้ง่ายขึ้นมาก ทำแค่ลาก Object parent มาตัวเดียวคือเสร็จเลย 

public class PositionData
{
    public Vector3 startPosition;
    public Quaternion startRotation;
    public BoxCollider colider;
    public Rigidbody rigidbody;

    public PositionData(Vector3 startPosition, Quaternion startRotation, BoxCollider colider, Rigidbody rigidbody)
    {
        // in case it recieve seperate parameter
        this.startPosition = startPosition;
        this.startRotation = startRotation;
        this.colider = colider;
        this.rigidbody = rigidbody;
    }

    public PositionData(Transform trans)
    {
        void DisableCollisionAndGravityAtStart()
        {
            this.colider.isTrigger = true;
            this.rigidbody.useGravity = false;
        }

        T GetCreateComponent<T>(Transform _transform) where T : Component
        {
            if (_transform.GetComponent<T>()) return _transform.GetComponent<T>();
            return _transform.gameObject.AddComponent<T>();
        }

        this.startPosition = trans.position;
        this.startRotation = trans.rotation;
        this.colider = GetCreateComponent<BoxCollider>(trans);
        this.rigidbody = GetCreateComponent<Rigidbody>(trans);


        DisableCollisionAndGravityAtStart();

    }

}

public class SeparateManager : MonoBehaviour
{
    [Header("Seperate Object")]
    [SerializeField] Transform parentObject;
    Transform[] _allTrans;
    [SerializeField] Ease easeType;
    [Tooltip("ของที่แยกแต่ละชิ้นจะห่างกันออกไปกี่หน่วย")]
    [SerializeField] float increase;

    float timeBetweenAll = 0.2f;

    [Tooltip("ของแต่ละชิ้นเวลาแยก ใช้เวลาชิ้นละกี่วินาที มันจะเปลี่ยนตาม Slider อยู่ดีนะ เช่น 0.2 = เลื่อน 1 ชิ้นทุก 0.2 วินาที / 1 = เลื่อน 1 ชิ้นทุก 1 วินาที ")]
    [SerializeField] float staticTimeDuration = 0.2f;

    [Tooltip("เวลาเราติ๊กให้ทำทีเดียวพร้อมกันหมด มันเร็วกว่าทำทีละชิ้นกี่เท่า")]
    [SerializeField] float allAtOnceMultiple;

    [Tooltip("ทำทีเดียวพร้อมกันหมดไหม - มันจะเปลี่ยนตามปุ่ม Toggle อยู่ดี")]
    [SerializeField] bool allAtOnce;

    [Header("UI")]
    [Tooltip("ปุ่ม Toggle ไว้สลับว่าทำทีเดียวหมดหรือทีละชิ้น")]
    [SerializeField] Toggle _toggle;

    [Tooltip("Slider คุมความเร็วในการแยก")]
    [SerializeField] Slider _slider;

    [Tooltip("ลากปุ่มหลักมาใส่อันนี้")]
    [SerializeField] Button _seperateButton;

    [Tooltip("ปุ่มเปิดปิดโน้มถ่วง")]
    [SerializeField] Button _gravityBtn;

    Image _btnSeperateImg;
    TMP_Text _btnSeperateText;

    Image _btnGravityImg;
    TMP_Text _btnGravityText;

    [Tooltip("สีปุ่มที่เปลี่ยนหลังกด")]
    [SerializeField] Color _blueCol;
    [Tooltip("สีปุ่มที่เปลี่ยนหลังกด")]
    [SerializeField] Color _redCol;

    List<PositionData> startData = new List<PositionData>();

    bool isAlreadySep;
    bool isGravityOn;

    float startOffset = 0;


    private void Start()
    {
        _allTrans = parentObject.GetComponentsInChildren<Transform>();
        RemoveAt(ref _allTrans, 0);


        DataSetup();
        ButtonSetup();


        _slider.onValueChanged.AddListener((v) =>
        {
            timeBetweenAll = staticTimeDuration / v;
        });
        timeBetweenAll = staticTimeDuration / _slider.value;
    }

    void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    void ButtonSetup()
    {
        _btnSeperateImg = _seperateButton.GetComponent<Image>();
        _btnSeperateText = _seperateButton.GetComponentInChildren<TMP_Text>();

        _btnGravityImg = _gravityBtn.GetComponent<Image>();
        _btnGravityText = _gravityBtn.GetComponentInChildren<TMP_Text>();

        _seperateButton.onClick.AddListener(buttonDoSep);
        _gravityBtn.onClick.AddListener(ToggleGravity);

        _gravityBtn.interactable = false;


        SetButtonVisualStartSep();
    }

    void DataSetup()
    {
        foreach (Transform _tran in _allTrans)
        {
            PositionData nowData = new PositionData(_tran);
            startData.Add(nowData);
        }
    }

    void SetBoxTriggerAndKine(bool _boxState, bool _kineState)
    {
        foreach (PositionData data in startData)
        {
            data.colider.isTrigger = _boxState;
            data.rigidbody.isKinematic = _kineState;
        }
    }

    #region seperateBtn
    void ChangeButtonState()
    {
        if (isAlreadySep)
        {
            SetButtonVisualUndoSep();
        }
        else
        {
            SetButtonVisualStartSep();
        }
    }
    void SetButtonVisualStartSep()
    {
        _btnSeperateText.SetText("START\n<size=17>separate</size>");
        _btnSeperateImg.color = _blueCol;
    }
    void SetButtonVisualUndoSep()
    {
        _btnSeperateText.SetText("REWIND\n<size=17>separate</size>");
        _btnSeperateImg.color = _redCol;
    }
    #endregion

    #region gravityBtn

    void TurnGravity(bool state)
    {
        foreach (PositionData data in startData)
        {
            data.rigidbody.useGravity = state;
        }
    }

    void SetGravityBtnAs(Color _col, string _word)
    {
        _btnGravityImg.color = _col;
        _btnGravityText.SetText(_word);
    }

    void ToggleGravity()
    {
        if (isGravityOn)
        {
            TurnOffGravity();
        }
        else
        {
            TurnOnGravity();
        }

        isGravityOn = !isGravityOn;
    }

    void TurnOffGravity()
    {
        TurnGravity(false);
        SetGravityBtnAs(_blueCol, "OFF");
    }
    void TurnOnGravity()
    {
        TurnGravity(true);
        SetGravityBtnAs(_redCol, "ON");
    }





    #endregion

    void SeperateAll()
    {
        _gravityBtn.interactable = false;
        allAtOnce = _toggle.isOn;

        // นับจำนวนของว่ามีกี่ชิ้น แล้วหาตรงกลางของของทั้งหมด
        startOffset = -(_allTrans.Length) * .5f * increase;

        void SeperateAllAtOnce()
        {
            // อันนี้คือมันจะทำการแยกพร้อมกันทุกอันทีเดียว
            _seperateButton.interactable = false;
            foreach (var _tran in _allTrans)
            {
                _tran.DOMoveX(_tran.position.x + startOffset, timeBetweenAll * (1 / allAtOnceMultiple)).SetEase(easeType);
                startOffset += increase;
            }
            _seperateButton.interactable = true;
        }
        void SeperateIndividual()
        {
            // อันนี้คือมันจะทำการแยกทีละอัน
            _seperateButton.interactable = false;
            var sequence = DOTween.Sequence();
            foreach (var _tran in _allTrans)
            {
                sequence.Append(_tran.DOMoveX(_tran.position.x + startOffset, timeBetweenAll).SetEase(easeType));
                startOffset += increase;
            }
            sequence.OnComplete(() =>
            {
                _seperateButton.interactable = true;
                SetBoxTriggerAndKine(false, false);
                _gravityBtn.interactable = true;
            });
        }

        if (allAtOnce)
        {
            SeperateAllAtOnce();
        }
        else
        {
            SeperateIndividual();
        }
    }

    void UndoSeperate()
    {
        SetBoxTriggerAndKine(true, true);  // kinematic to prevent move
        _gravityBtn.interactable = false;
        allAtOnce = _toggle.isOn;

        void UndoAllAtOnce()
        {
            _seperateButton.interactable = false;
            for (int i = 0; i < _allTrans.Length; i++)
            {
                _allTrans[i].DOMove(startData[i].startPosition, timeBetweenAll * (1 / allAtOnceMultiple)).SetEase(easeType);
                _allTrans[i].DORotateQuaternion(startData[i].startRotation, timeBetweenAll * (1 / allAtOnceMultiple)).SetEase(easeType);
            }
            _seperateButton.interactable = true;
        }

        void UndoIndividual()
        {
            _seperateButton.interactable = false;
            var sequence = DOTween.Sequence();

            for (int i = 0; i < _allTrans.Length; i++)
            {
                sequence.Append(_allTrans[i].DOMove(startData[i].startPosition, timeBetweenAll * 0.5f).SetEase(easeType));
                sequence.Append(_allTrans[i].DORotateQuaternion(startData[i].startRotation, timeBetweenAll * 0.5f).SetEase(easeType));
            }
            sequence.OnComplete(() =>
            {
                _seperateButton.interactable = true;
            });
        }

        if (allAtOnce)
        {
            UndoAllAtOnce();
        }
        else
        {
            UndoIndividual();
        }


    }

    public void buttonDoSep()
    {
        if (isAlreadySep)
        {
            UndoSeperate();
        }
        else
        {
            SeperateAll();
        }
        isAlreadySep = !isAlreadySep;
        ChangeButtonState();

    }




}
