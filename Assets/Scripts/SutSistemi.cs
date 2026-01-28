using UnityEngine;
using UnityEngine.UI; // Slider için şart

public class SutSistemi : MonoBehaviour
{
    [Header("Gerekli Bağlantılar")]
    public GameObject top;           // Topu buraya sürükle
    public Slider ibreSlider;        // Slider'ı buraya sürükle

    [Header("Atış Ayarları")]
    public float ileriGuc = 13f;     // Topun ileri gitme gücü
    public float yukariGuc = 11f;    // Topun havaya kalkma gücü
    public float ibreHizi = 1.5f;    // Barın hareket hızı
    public float gucCarpani = 2.0f;  // Güç çarpanı (Elleme)

    private bool atisYapildi = false;
    private Rigidbody rb;

    void Start()
    {
        // Topun fiziğini al ve yerçekimini kapat (Havada dursun)
        rb = top.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        // Eğer henüz ateş etmediysek
        if (!atisYapildi)
        {
            // 1. İbreyi Otomatik Hareket Ettir (PingPong)
            if (ibreSlider != null)
            {
                ibreSlider.value = Mathf.PingPong(Time.time * ibreHizi, 1.0f);
            }

            // 2. Tıklayınca Ateş Et
            if (Input.GetMouseButtonDown(0))
            {
                AtisYap();
            }
        }
    }

    void AtisYap()
    {
        atisYapildi = true;
        rb.useGravity = true; // Top artık yere düşebilir

        // Barın o anki değerini alıp güçle çarpıyoruz
        float vurusGucu = ibreSlider.value * gucCarpani;

        // X=0 yaptık ki sağa sola gitmesin, dümdüz gitsin
        Vector3 kuvvet = new Vector3(0, yukariGuc * vurusGucu, ileriGuc * vurusGucu);

        rb.AddForce(kuvvet, ForceMode.Impulse);
    }
}