using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public static AudioClip beep;
    public static AudioClip pop;
    public static AudioClip hit;
    public static AudioClip money;
    public static AudioClip dash;

    static AudioSource src;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        beep = Resources.Load<AudioClip>("beep");
        pop = Resources.Load<AudioClip>("pop");
        hit = Resources.Load<AudioClip>("hit");
        money = Resources.Load<AudioClip>("money");
        dash = Resources.Load<AudioClip>("dash");

        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "beep":
                src.PlayOneShot(beep, 1.0f);
                break;
            case "pop":
                src.PlayOneShot(pop, 0.9f);
                break;
            case "hit":
                src.PlayOneShot(hit, 0.7f);
                break;
            case "money":
                src.PlayOneShot(money, 0.7f);
                break;
            case "dash":
                src.PlayOneShot(dash, 1.0f);
                break;
        }
    }
}
