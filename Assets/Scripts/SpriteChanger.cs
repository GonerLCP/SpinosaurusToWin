using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite BaseSpinoWoSkate;
    public Sprite JumpWindup;
    public Sprite DuringJump;
    public Sprite OllieWindup;
    public Sprite DuringOllie;
    public Sprite Dead;
    public Sprite Scream;
    public Sprite SpeedingWindup;
    public Sprite DuringSpeeding;

    public Sprite BaseSkate;
    public Sprite SkateOllieWindup;

    public GameObject Player;
    SpriteRenderer PlayerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerSprite = Player.GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.BaseSpinoWoSkate:
                PlayerSprite.sprite = BaseSpinoWoSkate;
                break;
            case PlayerState.JumpWindup:
                PlayerSprite.sprite = JumpWindup;
                break;
            case PlayerState.DuringJump:
                PlayerSprite.sprite =DuringJump;
                break;
            case PlayerState.OllieWindup:
                PlayerSprite.sprite = OllieWindup;
                break;
            case PlayerState.DuringOllie:
                PlayerSprite.sprite = DuringOllie;
                break;
            case PlayerState.Dead:
                PlayerSprite.sprite =Dead;
                break;
            case PlayerState.SpeedingWindup:
                PlayerSprite.sprite = SpeedingWindup;
                break;
            case PlayerState.DuringSpeeding:
                PlayerSprite.sprite =DuringSpeeding;
                break;
            case PlayerState.Screaming:
                PlayerSprite.sprite = Scream;
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
