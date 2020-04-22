using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonasanjeNeprijatelja : MonoBehaviour {

    public AudioClip zvukPucnja;
    public AudioClip zvukUnistenja;
    public GameObject projektil;
    public float snaga = 150;
    public float PucanjuSekundi = 45;
    public float brzinaProjektila = 10;
    public int rezultatValue = 150;
    private PrikazRezultata prikazRezultata;

    void Start()
    {
        prikazRezultata = GameObject.Find("Rezultat").GetComponent<PrikazRezultata>();
    }

    void Update()
    {
        float vjerojatnost = PucanjuSekundi * Time.deltaTime;
        if(Random.value < vjerojatnost)
        {
            Fire();
        }
    }
    void Fire ()
    {
        Vector3 offset = new Vector3(0, -1.0f, 0);
        Vector3 polozajPucanja = transform.position + offset;
        GameObject missile = Instantiate(projektil, polozajPucanja, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -brzinaProjektila);
        AudioSource.PlayClipAtPoint(zvukPucnja, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projektil missile = collision.gameObject.GetComponent<Projektil>();
        if(missile)
        {
            missile.Hit();
            snaga -= missile.GetDamage();
            if(snaga <= 0)
            {
                AudioSource.PlayClipAtPoint(zvukUnistenja, transform.position);
                Die();
                Destroy(gameObject);
            }
        }
    }
    void Die()
    {
        prikazRezultata.Rezultat(rezultatValue);
        Destroy(gameObject);
    }
}
