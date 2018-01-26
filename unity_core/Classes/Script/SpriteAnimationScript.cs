using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 场景2d动画
/// @author hannibal
/// @time 2016-12-11
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SceneSpriteAnimation : UIComponentBase
{
    private SpriteRenderer mImageSource;
    private int mCurFrame = 0;
    private float mDelta = 0;
    private bool mIsPlaying = false;

    public List<Sprite> SpriteFrames;
    public float FPS = 12;
    public bool AutoPlay = true;
    public bool Loop = true;
    public bool Foward = true;

    public override void Awake()
    {
        mImageSource = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (AutoPlay)
        {
            this.Play();
        }
        else
        {
            mIsPlaying = false;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (AutoPlay)
        {
            this.Rewind();
        }
        else
        {
            mIsPlaying = false;
        }
    }

    public override void OnDisable()
    {
        this.Stop();
        base.OnDisable();
    }

    void Update()
    {
        if (!mIsPlaying || 0 == FrameCount)
        {
            return;
        }

        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            if (Foward)
            {
                mCurFrame++;
            }
            else
            {
                mCurFrame--;
            }

            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
                    mIsPlaying = false;
                    return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
                    mIsPlaying = false;
                    return;
                }
            }

            SetSprite(mCurFrame);
        }
    }
    /// <summary>
    /// 播放
    /// </summary>
    public void Play()
    {
        mIsPlaying = true;
        Foward = true;
    }
    /// <summary>
    /// 倒放
    /// </summary>
    public void PlayReverse()
    {
        mIsPlaying = true;
        Foward = false;
    }
    /// <summary>
    /// 重新播放
    /// </summary>
    public void Rewind()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        Play();
    }
    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        mIsPlaying = false;
    }
    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        mIsPlaying = false;
    }
    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume()
    {
        if (!mIsPlaying)
        {
            mIsPlaying = true;
        }
    }

    private void SetSprite(int idx)
    {
        mImageSource.sprite = SpriteFrames[idx];
    }

    public int FrameCount
    {
        get
        {
            return SpriteFrames.Count;
        }
    }
}