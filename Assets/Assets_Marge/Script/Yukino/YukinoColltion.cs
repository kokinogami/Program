using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class YukinoMain : MonoBehaviour
{
    Vector3 planeVector;
    float DamageCount;//�_���[�W���A�N�V���������p
    // Start is called before the first frame update
    void CollitionStart()
    {
        ItemCountUI.text = "�~" + CollectibleItem;
    }

    // Update is called once per frame
    void CollirionUpdate()
    {
        /*if (tenjou == false)
        {
            GameManager.Sound.SetCueName("Wind1");
            GameManager.Sound.WindNextBlock();
        }*/
    }
    void CollirionLateUpdate()
    {
        OnGroundJudge();
        DamageReactionJudge();
    }
    public void TridderCollectibleItem(GameObject Item)
    {
        CollectibleItem++;
        ItemCountUI.text = "�~" + CollectibleItem;
        Destroy(Item);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpPad" && index == 1)//�W�����v��
        {
            if (connectCoolTime < 0)
            {
                Sound.SetCueName("Jump_Dai");
                Sound.OnSound();
                Sound.SetCueName("Jump_Bass_Cat");
                Sound.Bass_Cut();
            }
            Ground = false;
            connectCoolTime = connectCoolTimeBackUp;
            rayCastCoolTime = 0.3f;
        }
        if (collision.gameObject.tag == "BrokenObject")
        {
            if (index == 1)
            {
                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);//�G�t�F�N�g�̑傫���ݒ�

            }
        }
        if (collision.gameObject.tag == "DeathZone")//��������ƃ��X�|�[������
        {
            this.transform.position = Startpos;
            Time.timeScale = 1.0f;
        }
        //if (collision.gameObject.tag == "zako") Debug.Log("ZAKO");
        if (collision.gameObject.tag == "JumpPad" && currentState == stateHipdrop)//���������ύX�\��
        {
            Destroy(collision.gameObject);
            rayCastCoolTime = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//�n�ʂƂ̐ڐG����
        {
            Ground = false;
        }
        if (collision.gameObject.tag == "JumpPad")//�W�����v��
        {
            if (currentState == stateRunning || currentState == stateEnergyDush)
            {
                rb.velocity = new Vector3(rb.velocity.x, 30, rb.velocity.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("heal") == true)//�񕜃A�C�e��
        {
            HP += 100;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("joushou"))
        {
            if (joushou) return;
            joushou = true;
            GameManager.Sound.SetCueName("Wind1"); //�㏸�C���ɓ������Ƃ�
            GameManager.Sound.WindSound();
            Debug.Log("A");
        }
        if (other.CompareTag("tenjou"))
        {
            tenjou = true;
        }
        if (other.CompareTag("checkPoint"))//���X�|�[���n�_�̕ύX
        {
            Startpos = other.transform.position;
        }
        if (other.CompareTag("DeathZone"))//��������ƃ��X�|�[������
        {
            OnWhiteFade = true;
            fadeInOut_W.gameObject.SetActive(true);
        }
        /*if (other.tag == "CollectibleItem")
        {
            CollectibleItem++;
            ItemCountUI.text = "�~" + CollectibleItem;
            Destroy(other.gameObject);
        }*/
        if (other.tag == "enemy") if (index == 1) CameraCs.ShakeCamera(ShakeCameraState.hitEnemy);
        //if (other.tag == "zako") Debug.Log("ZAKO");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("joushou"))
        {
            if (joushou)
            {
                joushou = false;
                GameManager.Sound.SetCueName("Wind1");//�㏸�C������o����
                GameManager.Sound.WindNextBlock();
            }
        }

        if (other.CompareTag("tenjou"))
        {
            tenjou = false;
        }
        if (other.CompareTag("Ground"))
        {
            Ground = false;
        }
    }

    public void OnGroundJudge()
    {
        if (rayCastCoolTime <= 0)
        {
            if (Physics.SphereCast(this.transform.position, groundRadius, Vector3.down, out RaycastHit hit, groundDistance))
            {
                if (hit.collider.gameObject.layer == 6) { Ground = false; return; }
                if (currentState == stateHipdrop && hit.collider.tag == "BrokenFloor") { Ground = false; return; }

                Ground = true;
                //Ray������������Ground == true�A�����łȂ����false
                Grider = false;
                //rb.drag = 0.0f;
                Jump = false;
                DoubleJump = false;
                DoubleJumpPossible = true;
                //griderObj.SetActive(false);
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Ground" || hit.collider.tag == "JumpPad")
                {
                    planeVector = Vector3.up;
                }
                else
                {
                    if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1.2f))
                    {
                        planeVector = hit.normal;
                    }
                    else
                    {
                        planeVector = Vector3.up;
                    }
                }


                if (Physics.Raycast(this.transform.position + transform.forward, Vector3.down, 2.0f) &&
                    Physics.Raycast(this.transform.position + transform.right, Vector3.down, 2.0f) &&
                    Physics.Raycast(this.transform.position + (-1 * transform.right), Vector3.down, 2.0f))
                {
                    if (hit.normal.y > 0.9f && hit.collider.tag == "RespawnableStage")
                        Startpos = transform.position + Vector3.up * 3.0f;
                }
            }
            else Ground = false;
        }
        yukinoanime.Groundbool(Ground);
        rayCastCoolTime -= Time.deltaTime;
    }

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        ChangeState(stateIdle);
        ChangeState(stateLoad);
        Grider = false;
        Jump = false;
        DoubleJump = false;
        DoubleJumpPossible = true;
        Hipdrop = false;
        //griderObj.SetActive(false);
        this.transform.position = Startpos;
    }
    public void OnDamage(int Damage, bool invincible)
    {
        if (GameManager.gameState == GameState.gameover || invinciblePlayer) return;
        if (HP == 0)
        {
            OpenGameOverMenu();
            return;
        }
        HP -= Damage;
        Sound.SetCueName("Damage");
        Sound.OnSound();


        //�_���[�W���A�N�V����
        DamageCount = DamagereactionTime;
        Instantiate(DamageEf, this.transform.position, Quaternion.identity);
        for (int i = 0; i < FirstMats.Length; i++)
        {
            CurrentMats[i] = DamageMat;
        }
        YukinoRenderer.materials = CurrentMats;

        for (int i = 0; i < SBFirstMats.Length; i++)
        {
            SBCurrentMats[i] = DamageMat;
        }
        SnowBallRenderer.materials = SBCurrentMats;


        if (HP < 0) HP = 0;
        attackedAuteHealCount = attackedHealTimeCount;

        if (invincible)
        {
            StartCoroutine("invincibleTime", 3);
        }
    }
    IEnumerator invincibleTime(float second)
    {
        invinciblePlayer = true;
        yield return new WaitForSeconds(second);
        invinciblePlayer = false;
    }
    void OpenGameOverMenu()
    {
        //GameManager.gameState = GameState.gameover;
        GameManager.Sound.SetCueName("Game_Over");
        GameManager.Sound.OnSound();

        Gamemanager.GameOverMenu();
        _input.SwitchCurrentActionMap("UI");
        CameraCs.CameraMove(false);
        //Destroy(this.gameObject);
        Instantiate(GameManager.Main.SnowballReleaseEf, GameManager.Main.gameObject.transform.position, Quaternion.identity);
        childObject[0].SetActive(false);
        childObject[1].SetActive(false);
    }

    private void DamageReactionJudge()//�_���[�W���A�N�V�����̌p������
    {
        if (DamageCount > 0)
        {
            DamageCount -= Time.deltaTime;
        }
        else if (CurrentMats[0] != FirstMats[0])
        {
            for (int i = 0; i < FirstMats.Length; i++)
            {
                CurrentMats[i] = FirstMats[i];
            }
            YukinoRenderer.materials = CurrentMats;
        }

        if (DamageCount <= 0 && SBCurrentMats[0] != SBFirstMats[0])
        {
            for (int i = 0; i < SBFirstMats.Length; i++)
            {
                SBCurrentMats[i] = SBFirstMats[i];
            }
            SnowBallRenderer.materials = SBCurrentMats;
        }
    }
}