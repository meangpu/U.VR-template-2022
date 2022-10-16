using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float bulletSpeed = 30;
    [SerializeField] float bulletLifeTimeSec = 8;

    private void Start()
    {
        XRGrabInteractable grabable = GetComponent<XRGrabInteractable>();
        grabable.activated.AddListener(FireBullet);

    }

    public void FireBullet(ActivateEventArgs arg)
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = spawnPoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
        Destroy(newBullet, bulletLifeTimeSec);
    }

}