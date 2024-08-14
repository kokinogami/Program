using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dou_ene_Empty : MonoBehaviour
{
    public Enemy2 script;
    public float Passtime = 0f;
    public float waitpasstime = 0f;
    public float atpasstime = 0f;
    public float movetime = 1.5f;
    float horiRate = 1.0f;
    float attackRate = 1.0f;
    float waitRate = 1.0f;
    public bool compHo;
    public bool compWa;
    public bool compAt;
    Quaternion preangle;
    Quaternion horiangle;
    Quaternion attackangle;
    Quaternion waitangle;
    void Start()
    {
      
        
    }
     void Update()
    {
      /*  if (Passtime >= movetime)
        {
            compHo = true;
        }
        else { compHo = false; }
        if (waitpasstime >= movetime)
        {
            compWa = true;
        }
        else { compWa = false; }
        if (atpasstime >= movetime)
        {
            compAt = true;
        }
        else { compAt = false; }*/
    }
    // Update is called once per frame
    void FixedUpdate()
    { 
        if(script.rush == true)
        {
            transform.Rotate(new Vector3(0, script.Roundspheremove * 4.0f, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, script.Roundspheremove, 0));//Ç∏Ç¡Ç∆ÇÆÇÈÇÆÇÈÅ`  
        }
    }
    public void horizon()
    {
        transform.rotation = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y, 0.0f);//ïΩçsÇ…íºÇ∑
         /* preangle =this.transform.rotation;
          horiangle = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y, 0.0f);//ïΩçsÇ…íºÇ∑
          Passtime += Time.deltaTime;
          horiRate = Mathf.Clamp01(Passtime / movetime);
          transform.rotation =Quaternion.Slerp(preangle, horiangle, horiRate);    */
    }
    public void waittilt()
    {
       /* preangle = this.transform.rotation;
        waitpasstime += Time.deltaTime;
        //waitangle = Quaternion.AngleAxis(30f,Vector3.right);//éŒÇﬂÇÃäpìxÇ…Ç∑ÇÈ
        waitangle = Quaternion.Euler(this.transform.rotation.x +30.0f, this.transform.rotation.y, this.transform.rotation.z);
        waitRate = Mathf.Clamp01(waitpasstime / movetime);
        transform.rotation = Quaternion.Slerp(preangle, waitangle, waitRate);*/
        transform.localRotation = Quaternion.Euler(30f, 0, 0);//éŒÇﬂÇÃäpìxÇ…Ç∑ÇÈ
    }
    public void attacktilt()
    {
       /* preangle = this.transform.rotation;
        atpasstime += Time.deltaTime;
        //attackangle = Quaternion.AngleAxis(10f, Vector3.right);
        attackangle = Quaternion.Euler(10.0f, this.transform.rotation.y, this.transform.rotation.z);
        attackRate = Mathf.Clamp01(atpasstime / movetime);
        transform.rotation = Quaternion.Slerp(preangle, attackangle, attackRate);*/
        transform.localRotation = Quaternion.Euler(10f, 0, 0);
    }
}
