using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// </summary>
public class GameMainPanel : BasePanel
{
    //人物头像
    public Slider hpSlider;
    public Slider mpSlider;
    public Image heroImg;
    //攻击按钮
    public Button attackBtn;
    public Image attackBg;
    //聊天
    public GameObject content;
    public Toggle contentTog;
    public Animator contentAnimator;
    //人物属性
    public Toggle peopleTog;
    public Button knapsackBtn;
    //任务栏
    public Toggle taskTog;
    public Button shopBtn;
    public override void OnInit(){
        skinPath = "GameMainPanel";
        layer = PanelManager.Layer.Panel;
    }

    public override void OnShow(params object[] para){
        Transform heroHPMPTrans = skin.transform.Find("HeroHPMP");
        heroImg = heroHPMPTrans.GetChild(0).GetComponent<Image>();
        hpSlider = heroHPMPTrans.GetChild(1).GetComponent<Slider>();
        mpSlider = heroHPMPTrans.GetChild(2).GetComponent<Slider>();

        attackBtn = skin.transform.Find("AttackBtn").GetComponent<Button>();

        Transform scrollTrans = skin.transform.Find("Communication");
        Transform viewTrans = scrollTrans.Find("Viewport");
        content = viewTrans.gameObject;
        contentAnimator = scrollTrans.GetComponent<Animator>();
        contentTog = scrollTrans.GetComponentInChildren<Toggle>();

        Transform peopleTrans = skin.transform.Find("People");
        peopleTog = peopleTrans.GetChild(0).GetComponent<Toggle>();
        knapsackBtn = peopleTrans.GetChild(1).GetComponent<Button>();
        knapsackBtn.onClick.AddListener(OnClickKnapsack);

        Transform taskTrans = skin.transform.Find("Task");
        taskTog = taskTrans.GetChild(0).GetComponent<Toggle>();
        shopBtn = taskTrans.GetChild(1).GetComponent<Button>();
        shopBtn.onClick.AddListener(OnClickShop);

        //聊天栏位缩放
        contentTog.onValueChanged.AddListener(OnContentTogValueChanged);
        //血量变化监听
        PlayerAtt.PlayerHp += HpChanged; 
        //蓝量变化监听
        PlayerAtt.PlayerMp += MpChanged;
    }

    public override void OnClose(){
        knapsackBtn.onClick.RemoveListener(OnClickKnapsack);
        shopBtn.onClick.RemoveListener(OnClickShop);
        PlayerAtt.PlayerHp -= HpChanged;
        PlayerAtt.PlayerMp -= MpChanged;
    }

    public void OnClickKnapsack(){
        if(PanelManager.IsHide("KnapsackPanel")){
            PanelManager.Hide("KnapsackPanel");
        }else{
            PanelManager.UnHide("KnapsackPanel");
        }
    }

    public void OnClickShop(){
        if(PanelManager.IsHide("ShopPanel")){
            PanelManager.Hide("ShopPanel");
        }else{
            PanelManager.UnHide("ShopPanel");
        }
    }

    private void HpChanged(long hp, long maxHp){
        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;
    }

    private void MpChanged(long mp, long maxMp){
        mpSlider.maxValue = maxMp;
        mpSlider.value = mp;
    }

    private void OnContentTogValueChanged(bool hided){
        contentAnimator.SetBool("hide",hided);
    }
}
