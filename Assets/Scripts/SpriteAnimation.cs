using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour {

    public Sprite[] sprites; //存放用于做动画的图片帧

    [Tooltip("动画的播放速度，表示每帧图片切换的间隔时间")]
    public float animaPlaySpeed=0.2f;

    private Image image;  //该对象的Image组件
    
	void Start () {
        FindSprites();//初始化获取对应的图片集
        image = this.GetComponent<Image>();
	}

    //在文件夹动态加载对应的图片集用于做动画
    private void FindSprites()
    {
        //根据对象的名字找对应名字的图片集
        int length = Resources.LoadAll(this.name,typeof(Sprite)).Length;
        sprites = new Sprite[length];

        for(int i = 0; i < length; i++)
        {
            sprites[i] = (Sprite)Resources.LoadAll(this.name, typeof(Sprite))[i];
        }
    }

    //调用开始播放动画
    public void PlayAnimation()
    {
        StopCoroutine("animaPlay");
        StartCoroutine("animaPlay");
    }

    //调用停止播放动画
    public void StopAnimation()
    {
        StopCoroutine("animaPlay");
    }

    //调用协程循环切换图片，相当于播放动画
    IEnumerator animaPlay()
    {
        int index = 0;
        int length = sprites.Length;

        while (true)
        {
            image.sprite = sprites[index];
            index++;
            if (index >= length)
            {
                index = 0;
            }
            yield return new WaitForSeconds(animaPlaySpeed);  //暂停一段时间再切换下一张图片
        }
    }
}
