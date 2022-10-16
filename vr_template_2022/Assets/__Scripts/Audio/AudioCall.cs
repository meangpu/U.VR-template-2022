using UnityEngine;

public class AudioCall : MonoBehaviour
{
    // ใช้กะพวกปุ่มที่เชื่อม audio manage ตรงจาก scene ไม่ได้ 
    // ในกรณีที่ scene เปลี่ยนมาจาก mainmenu แล้ว audio manager reset
    AudioManager _aud;

    private void Start() 
    {
        _aud = FindObjectOfType<AudioManager>();
    }

    public void playSound(string _name)
    {
        _aud.Play(_name);
    }
}
